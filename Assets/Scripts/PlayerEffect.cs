using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PlayerEffect : MonoBehaviour
{
    PlayerControl control;
    SpriteRenderer sr;

    //public enum E_effect { None, ItemInvin, Avitaton }// 비행이랑 무적 변경
    public bool[] b_Effected = new bool[2]; // Effect 활성화 여부
    public GameObject[] go_Effects;         // Effect 오브젝트    
    Define.Effects curEffect;
    public float f_Magnetic_Rad;            // 자석 범위
    public float f_Invic_time;              // 피격 무적 시간
    public Color[] colors;                  // 피격 효과 색상
    // Start is called before the first frame update
    void Start()
    {
        control = GetComponent<PlayerControl>();
        sr = GetComponent<SpriteRenderer>();
        curEffect = Define.Effects.None;
    }

    // Update is called once per frame
    void Update()
    {
        if(b_Effected[1]) Avitation();// 자석 및 날기
    }
    public Action<Define.Effects> buffTimerCount;
    public void GetEffectItem(Action<Define.Effects> action)
    {
        buffTimerCount -= action;
        buffTimerCount += action;
    }
    public void Activate_Effect(Define.Effects type , float duration = 0)
    {
        ChangeEffect(type);
    }
    public void Call_InvincibleMode()
    {
        StartCoroutine(SetAttacked());
    }
    void ChangeEffect(Define.Effects effect)
    {
        curEffect = effect;
        switch (curEffect)
        {
            case Define.Effects.None:
                if (control.state == PlayerControl.E_State.Aviation) control.ChangeState(PlayerControl.E_State.Run);
                this.gameObject.layer = 0;
                break;
            case Define.Effects.Avitation:
                OnAvitation();
                buffTimerCount.Invoke(curEffect);
                //BuffManager.instance.TurnOnBuffTimer(E_BUFFTYPE.Aviation);
                break;
            case Define.Effects.Invincibility:
                OnInvincibility();
                buffTimerCount.Invoke(curEffect);
                //BuffManager.instance.TurnOnBuffTimer(E_BUFFTYPE.Invincibility);
                break;
        }
    }
    void ResetEffect()
    {
        for (int i = 0; i < 2; i++)
        {
            b_Effected[i] = false;
            go_Effects[i].SetActive(false);
        }
    }
    void OnAvitation()
    {
        ResetEffect();
        control.ChangeState(PlayerControl.E_State.Aviation);
        b_Effected[1] = true;
        go_Effects[1].SetActive(true);
        this.gameObject.layer = 9; // 9 여도 상관 없을듯
    }
    void OnInvincibility()
    {
        ResetEffect();
        b_Effected[0] = true;
        go_Effects[0].SetActive(true);
        this.gameObject.layer = 9;
    }
    public void OffEffect()
    {
        for (int i = 0; i < 2; i++)
        {
            b_Effected[i] = false;
            go_Effects[i].SetActive(false);
        }
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        ChangeEffect(Define.Effects.None);
    }
    IEnumerator SetAttacked()
    {
        //Debug.Log("Invincibility_Start");
        this.gameObject.layer = 9;
        float sec = f_Invic_time / 10;
        for (int i = 0; i < 10; i++)
        {
            sr.color = colors[i % 2];
            yield return new WaitForSeconds(sec);
        }
        if (curEffect == Define.Effects.None) this.gameObject.layer = 0;
        //Debug.Log("Invincibility_End");
    }
    void Avitation()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, f_Magnetic_Rad);
        foreach(Collider2D collider in colliders)
        {
            if(collider.GetComponent<Item>())
            {
                collider.GetComponent<Item>().SetTarget(this.gameObject);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, f_Magnetic_Rad);
    }
}
