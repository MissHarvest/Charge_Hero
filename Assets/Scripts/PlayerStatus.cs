using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStatus : MonoBehaviour
{
    Status status;
    public int HP { get { return status.hp; } set { status.hp = value; ChangeStatHandler.Invoke(); } }
    public int ATK { get { return status.atk; } set { status.atk = value; } }
    public int SHIELD { get { return status.shield; } set { status.shield = value; } } // 무적 Item 지속시간 증가
    
    PlayerControl control;
    PlayerEffect effect;

    // Start is called before the first frame update
    void Start()
    {
        // Init //
        control = GetComponent<PlayerControl>();
        effect = GetComponent<PlayerEffect>();
    }

    public void Set(UserInfo userinfo)
    {
        status.hp = userinfo.hp;
        status.atk = userinfo.atk;
        status.shield = userinfo.shield;
    }
    Action ChangeStatHandler;

    // player.GetComponent(PlayerStatus).AddAction(action)
    public void ChangeStatus(Action action)
    {
        this.ChangeStatHandler -= action;
        this.ChangeStatHandler += action;
    }
    public void Damaged(int dmg)
    {
        // StageManager.instance.attacked_Cnt++;
        if (SHIELD > 0)
        {
            SHIELD -= dmg;
            //GUIManager.instance.SHIELD_UI.Remove();
        }
        else
        {
            HP -= dmg;
            //GUIManager.instance.HP_UI.Remove();
        }

        if (HP <= 0)
        {
            control.anim.SetTrigger("doDie");
            Managers.Sound.Play("Fail");
            Managers.Sound.OffBGM();
            control.ChangeState(PlayerControl.E_State.End);
        }
        else effect.Call_InvincibleMode();

        ChangeStatHandler.Invoke();
    }
}
