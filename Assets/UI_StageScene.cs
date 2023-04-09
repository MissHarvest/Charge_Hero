using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_StageScene : UI_Scene
{
    enum Buttons
    {
        Back,
    }
    enum GameObjects
    {
        GridPanel,
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
        Bind<GameObject>(typeof(GameObjects));

        GetButton((int)Buttons.Back).gameObject.EventBind(ShowChapterScene);

        GameObject gridPanel = GetObject((int)GameObjects.GridPanel);
        foreach (Transform child in gridPanel.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }

        for (int i = 0; i < 5; i++)
        {
            GameObject go = Managers.UI.MakeSubUI<UI_Stage>(gridPanel.transform).gameObject;
            UI_Stage stage = go.GetOrAddComponet<UI_Stage>();
            stage.SetInfo($"STAGE {i + 1}", DataBase.GetStage(i));
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void ShowChapterScene(PointerEventData data)
    {
        Managers.UI.ShowSceneUI<UI_ChapterScene>();
    }

}
