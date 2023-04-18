using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStatus : MonoBehaviour
{
    // 멤버 변수 //
    PlayerControl   control;
    PlayerEffect    effect;
    [SerializeField] Status          status;
    Action          hpUIUpdate; // int 아니어도됨..
    Action<int>     atkUIUpdate;


    // 프로퍼티 //
    public int HP { get { return status.hp; } }
    public int ATK { get { return status.atk; } }
    public int SHIELD { get { return status.shield; } } // 무적 Item 지속시간 증가

    public void Set(UserInfo userinfo)
    {
        status.hp = userinfo.hp;
        status.atk = userinfo.atk;
        status.shield = userinfo.shield;
    }
    void Start()
    {
        // Init //
        control = GetComponent<PlayerControl>();
        effect = GetComponent<PlayerEffect>();
    }
    public void HpChanged(Action _action)
    {
        hpUIUpdate -= _action;
        hpUIUpdate += _action;
    }
    public void AtkChanged(Action<int> action)
    {
        this.atkUIUpdate -= action;
        this.atkUIUpdate += action;
    }
    public void Damaged(int dmg)
    {
        if (SHIELD > 0)
        {
            status.shield -= dmg;
        }
        else
        {
            status.hp -= dmg;
        }

        if (HP <= 0)
        {
            control.anim.SetTrigger("doDie");
            Managers.Sound.Play("Fail");
            Managers.Sound.OffBGM();
            control.ChangeState(PlayerControl.E_State.End);
        }
        else effect.Call_InvincibleMode();
        // Debug.Log(StageManager.Instance.gameObject.name);
        StageManager.Instance.PlayerAttacked();
        hpUIUpdate.Invoke();
    }
    // Recovery
    public void Recovery()
    {
        if (HP < DataBase.Get<UserInfo>().hp)
            status.hp++;
        hpUIUpdate.Invoke();
    }
    // PowerUP
    public void PowerUP(int power)
    {
        atkUIUpdate.Invoke(power);
        status.atk += power;
    }

}
