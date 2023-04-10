using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_GameplayPanel : UI_Scene
{
    enum Buttons
    {
        Jump_Btn,
        Slide_Btn,
    }
    enum Texts
    {
        PlayerAtk_Text,
    }
    enum GameObjects
    {
        BossHP_UI,
        Dist_UI,
        HP_UI,
        SHIELD_UI,
    }
    [SerializeField] GameObject player;
    [SerializeField] GameObject boss;
    PlayerControl controler;

    private void Start()
    {
        Init();
    }
    protected override void Init()
    {
        base.Init();

        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        GetButton((int)Buttons.Jump_Btn).gameObject.EventBind(controler.DoJump); // 이거 왜 지랄이야

        GetButton((int)Buttons.Jump_Btn).gameObject.EventBind((flag) => controler.isJumpBtnDown = flag);
        GetButton((int)Buttons.Slide_Btn).gameObject.EventBind((flag) => controler.isSlideBtnDown = flag);

        GetObject((int)GameObjects.Dist_UI).GetComponent<UI_Distance>().Set(player, boss);
        GetObject((int)GameObjects.BossHP_UI).GetComponent<UI_BossHP>().Set(boss);
        
        GetObject((int)GameObjects.HP_UI).GetComponent<UI_List>().Set(player, Define.E_Status.HP);
        GetObject((int)GameObjects.SHIELD_UI).GetComponent<UI_List>().Set(player, Define.E_Status.SHIELD);
    }
    public void Set(GameObject player, GameObject boss)
    {
        this.player = player;
        this.boss = boss;
        controler = player.GetComponent<PlayerControl>();
    }
}
