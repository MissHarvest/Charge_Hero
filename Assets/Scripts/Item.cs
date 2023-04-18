using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Heal,
    Damage,
    Invincibility,
    Aviation,
    ChangeCoin,
    Gold,
}

public class Item : MonoBehaviour
{
    public ItemType curType;
    public int value;
    public bool moved;
    public float speed;

    public GameObject buffTimer;
    //public float duration;
    //public float timer;

    GameObject go_Collider;
    private void Start()
    {
        
    }

    private void Update()
    {
        if (go_Collider) Move_To_Player();
    }
    public void SetTarget(GameObject player)
    {
        go_Collider = player;
    }
    void Move_To_Player()
    {
        Vector3 v_Dir = go_Collider.transform.position - transform.position;
        v_Dir.z = 0;
        v_Dir.Normalize();
        transform.position += v_Dir * speed * Time.deltaTime;
    }
    Action action;
    public void GetItemAction(Action _action)
    {
        action -= _action;
        action += _action;
    }
    void UseItem()
    {
        switch (curType)
        {
            case ItemType.Heal: // 힐
                go_Collider.GetComponent<PlayerStatus>().Recovery();
                break;

            case ItemType.Damage:// 파워업
                go_Collider.GetComponent<PlayerStatus>().PowerUP(value);
                break;

            case ItemType.Invincibility:// 무적
                go_Collider.GetComponent<PlayerEffect>().Activate_Effect(Define.Effects.Invincibility, value);
                break;

            case ItemType.Aviation:// 비행 + 무적 + 자석(골드만?)
                go_Collider.GetComponent<PlayerEffect>().Activate_Effect(Define.Effects.Avitation, value);
                break;

            case ItemType.ChangeCoin:
                //GameManager.instance.changeCoin = true;
                break;

            case ItemType.Gold: // 골드
                Managers.Sound.Play("GetCoin");
                DataBase.Get<UserInfo>().gold++;
                break;
        }

        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            go_Collider = collision.gameObject;
            UseItem();
        }
    }
}