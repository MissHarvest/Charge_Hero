using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Chapter : UI_Base
{
    enum Texts
    {
        Chapter_Text,
    }
    enum GameObjects
    {
        Blocker,
    }
    enum Buttons
    {
        Chapter_Btn,
    }
    
    string _name;
    Chapter _chapter;
    // Start is called before the first frame update
    public void SetInfo(string name, Chapter chapter)
    {
        this._name = name;
        gameObject.name = _name;
        this._chapter = chapter;
    }
    void Start()
    {
        Init(); // Init() 이 두번 불리는뎅
    }
    protected override void Init()
    {
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));

        GetText((int)Texts.Chapter_Text).text = $"<b><color=white>{_name}</color></b>";
        
        GetObject((int)GameObjects.Blocker).SetActive(!_chapter.isOpen);

        GetButton((int)Buttons.Chapter_Btn).gameObject.EventBind(ShowStageScene);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ShowStageScene(PointerEventData data)
    {
        Managers.UI.ShowSceneUI<UI_StageScene>();
    }
}
