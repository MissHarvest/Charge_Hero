using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class BossMonster : MonoBehaviour
{
    public GameObject goal;
    public float HP;
    GameObject go_Player;
    Animator anim;
    SpriteRenderer sr;
    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Log("BossMonster.Awake");
        goal.transform.position = new Vector3(transform.position.x + 5, -2.0f, transform.position.z);
        //StageManager.instance.go_Boss = this.gameObject;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    public Action<float> UIEnable;
    public void HpUIEnable()
    {
        UIEnable.Invoke(0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="Player")
        {
            go_Player = collision.gameObject;
            // go_Player.GetComponent<PlayerControl>().
            if (HP > go_Player.GetComponent<PlayerStatus>().ATK)
            {
                // SoundManager.instance.PlayEffect("Attacked");
                Managers.Sound.Play("Attacked");
                // ������ �����ϴ� Animation �߰� //
                go_Player.GetComponent<PlayerControl>().ChangeState(PlayerControl.E_State.Attacked);
                StageManager.Instance.kill = false;
            }
            else
            {
                // SoundManager.instance.PlayEffect("Attack");
                Managers.Sound.Play("Attack");
                
                // ������ ���� ���ϴ� Animation �߰� //
                anim.SetTrigger("doDie");
                sr.color = Color.gray;
                StageManager.Instance.kill = true;
            }
            HP -= go_Player.GetComponent<PlayerStatus>().ATK;
            if (HP < 0) HP = 0;
            UIEnable.Invoke(HP);
        }
    }
}
