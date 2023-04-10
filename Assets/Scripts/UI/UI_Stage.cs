using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Stage : UI_Base
{
    enum Texts
    {
        Stage_Text,
        Process_Text
    }
    enum GameObjects
    {
        Blocker,
        Stars,
    }
    enum Buttons
    {
        Stage_Btn,
    }
    string _name;
    Stage _stage;
    public void SetInfo(string name, Stage stage)
    {
        this._name = name;
        gameObject.name = _name;
        this._stage = stage;
    }
    private void Start()
    {
        // Debug.Log("Stage_Start");
        Init();
    }
    protected override void Init()
    {
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));

        GetText((int)Texts.Stage_Text).text = $"<b><color=white>{_stage.name}</color></b>";
        GetObject((int)GameObjects.Blocker).SetActive(!_stage.isOpen);
        GetObject((int)GameObjects.Stars).GetComponent<UI_Star>().ShowStar(_stage.getStar);
        GetText((int)Texts.Process_Text).text = string.Format("{0:F0} %", _stage.percent);
        GetButton((int)Buttons.Stage_Btn).gameObject.EventBind(ShowReadyScene);
    }
    void ShowReadyScene(PointerEventData data)
    {
        Managers.stage = this._stage;
        Managers.UI.ShowSceneUI<UI_ReadyScene>();
    }
}
