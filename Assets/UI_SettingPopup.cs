using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_SettingPopup : UI_Popup
{
    enum Buttons
    {
        Menu_Btn,
        Stage_Btn,
    }
    enum GameObjects
    {
        Blocker,
    }
    Define.Scene scene;
    protected override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        GetButton((int)Buttons.Menu_Btn).gameObject.EventBind(ShowTitleScene);
        GetButton((int)Buttons.Stage_Btn).gameObject.EventBind(ShowStageScene);

        GetObject((int)GameObjects.Blocker).EventBind(ClosePopup);

        bool enable = Convert.ToBoolean((int)scene);
        Debug.Log($"Scene to int {(int)scene} , enalbe {enable}");
        for(int i =0; i< 2; i++)
        {
            GetButton(i).gameObject.SetActive(enable);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
        Time.timeScale = 0;
    }
    public void Set(Define.Scene _scene)
    {
        scene = _scene;
    }
    void ClosePopup(PointerEventData data)
    {
        Time.timeScale = 1;
        Managers.UI.ClosePopupUI(this);
    }
    void ShowTitleScene(PointerEventData data)
    {
        Managers.UI.ShowSceneUI<UI_TitleScene>();
    }
    void ShowStageScene(PointerEventData data)
    {
        Managers.UI.ShowSceneUI<UI_StageScene>();
    }
}
