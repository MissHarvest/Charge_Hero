using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_GameplayPanel : UI_Popup
{
    enum Texts
    {
        PlayerAtk_Text,
        BossHp_Text,
    }
    private void Start()
    {
        Init();
    }
    protected override void Init()
    {
        base.Init();

        Bind<Text>(typeof(Texts));

        // Text text = Get<Text>((int)Texts.PlayerAtk_Text);
        GetObject((int)Texts.PlayerAtk_Text).EventBind(Test, Define.UIEvent.Click);
    }
    private void Test(PointerEventData eventdata)
    {

    }
}
