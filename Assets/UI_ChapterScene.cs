using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_ChapterScene : UI_Scene
{
    enum Buttons
    {
        Back,
    }
    enum GameObjets
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
        Bind<GameObject>(typeof(GameObjets));

        GetButton((int)Buttons.Back).gameObject.EventBind(ShowTitleScene);

        GameObject gridPanel = GetObject((int)GameObjets.GridPanel);

        // 사실 쓸모가 없는 코드가 아닐까. --> 오류가 발생해도 처리해주는 코드인가
        foreach (Transform child in gridPanel.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }

        for (int i = 0; i < 4; i++) // 4라는 숫자를 다른데서 읽어와야 하는가 ? 
        {
            GameObject go = Managers.UI.MakeSubUI<UI_Chapter>(gridPanel.transform).gameObject;
            UI_Chapter chapter = Util.GetOrAddComponent<UI_Chapter>(go);
            chapter.SetInfo($"CHAPTER {i + 1}", DataBase.Get<Chapter>(i));
        }
    }
    void ShowTitleScene(PointerEventData data)
    {
        Managers.UI.ShowSceneUI<UI_TitleScene>();
    }
    
}
