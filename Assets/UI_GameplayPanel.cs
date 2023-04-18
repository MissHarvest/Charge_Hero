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
        Setting_Btn,
    }
    enum Texts
    {
        PlayerAtk_Text,
    }
    enum GameObjects
    {
        BossHP_UI,
        HP_UI,
        SHIELD_UI,
    }
    [SerializeField] GameObject player;
    [SerializeField] GameObject boss;
    PlayerControl controler;
    PlayerStatus status;
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

        GetButton((int)Buttons.Setting_Btn).gameObject.EventBind(ShowSettingPopup);

        UI_Distance distUI = Managers.UI.MakeSubUI<UI_Distance>(this.transform);
        distUI.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 25);
        distUI.Set(player, boss);

        UI_Buff buffUI = Managers.UI.MakeSubUI<UI_Buff>(transform);
        buffUI.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(100, -50);
        buffUI.Set(player);

        GetObject((int)GameObjects.BossHP_UI).GetComponent<UI_BossHP>().Set(boss);
        
        GetObject((int)GameObjects.HP_UI).GetComponent<UI_List>().Set(player, Define.E_Status.HP);
        GetObject((int)GameObjects.SHIELD_UI).GetComponent<UI_List>().Set(player, Define.E_Status.SHIELD);
        GetText((int)Texts.PlayerAtk_Text).text = string.Format("{0:#,##0}", player.GetComponent<PlayerStatus>().ATK);
        status.AtkChanged(AtkUIUpdate);
    }
    public void Set(GameObject player, GameObject boss)
    {
        Debug.Log($"(UI_GameplayPanel)Recieved {player.name} , {boss.name}");
        this.player = player;
        this.boss = boss;
        controler = player.GetComponent<PlayerControl>();
        status = player.GetComponent<PlayerStatus>();
    }
    void AtkUIUpdate(int plusatk)
    {
        StartCoroutine(NumberAnimation(GetText((int)Texts.PlayerAtk_Text), status.ATK + plusatk, status.ATK, "{0:#,##0}"));
    }
    IEnumerator NumberAnimation(Text text, float target, float current, string format)
    {
        float duration = 0.5f;// f_delay; // 카운팅에 걸리는 시간 설정. 

        float offset = (target - current) / duration; // 
        
        while (current > target)
        {
            current += offset * Time.deltaTime;
            text.text = string.Format(format, (int)current);
            yield return null;
        }
        current = target;
        text.text = string.Format(format, (int)current);
    }
    void ShowSettingPopup(PointerEventData data)
    {
        Managers.UI.ShowPopupUI<UI_SettingPopup>().Set(Define.Scene.Game);
    }
}
