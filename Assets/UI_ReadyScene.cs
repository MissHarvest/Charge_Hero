using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UI_ReadyScene : UI_Scene
{
    enum Buttons
    { 
        Back,
        Start_Btn,
    }
    enum GameObjects
    {
        GridPanel,
    }
    Stage _stage;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("UI_ReadyScene Start");
        Init();
    }
    protected override void Init()
    {
        base.Init();
        _stage = Managers.stage;

        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        GetButton((int)Buttons.Back).gameObject.EventBind(ShowStageScene);
        GetButton((int)Buttons.Start_Btn).gameObject.EventBind(LoadGame);

        UI_Gold _gold = Managers.UI.MakeSubUI<UI_Gold>(this.gameObject.transform);
        _gold.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(450, -140);

        GameObject gridPanel = GetObject((int)GameObjects.GridPanel);
        foreach(Transform child in gridPanel.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }
        
        for(int i = 0; i < 3; i++)
        {
            GameObject go = Managers.UI.MakeSubUI<UI_Enhance>(gridPanel.transform).gameObject;
            // go.GetComponentInChildren<Button>().gameObject.EventBind(_gold.TE);
            UI_Enhance enhance = go.GetOrAddComponet<UI_Enhance>();
            _gold.enhanceUIs.Add(enhance);
            enhance.SetInfo($"Enhance({(Define.E_Status)i})", i);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowStageScene(PointerEventData data)
    {
        Managers.UI.ShowSceneUI<UI_StageScene>();
    }
    void LoadGame(PointerEventData data)
    {
        SceneManager.LoadScene("1-1");
        Managers.Resource.Destroy(this.gameObject);
    }

}
