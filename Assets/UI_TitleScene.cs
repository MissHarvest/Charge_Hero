using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class UI_TitleScene : UI_Scene
{
    enum Buttons
    {
        Start_Btn,
        Setting_Btn,
        Credit_Btn
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    protected override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        GetButton((int)Buttons.Start_Btn).gameObject.EventBind(ShowChapterScene);
        GetButton((int)Buttons.Setting_Btn).gameObject.EventBind(ShowSettingPopup);
        Managers.UI.ShowPopupUI<UI_BackGround>();
    }
    void ShowChapterScene(PointerEventData data)
    {
        Managers.UI.ShowSceneUI<UI_ChapterScene>();
        Managers.UI.ClosePopupUI();
    }
    void ShowSettingPopup(PointerEventData data)
    {
        Managers.UI.ShowPopupUI<UI_SettingPopup>().Set(Define.Scene.Home);
    }
}
