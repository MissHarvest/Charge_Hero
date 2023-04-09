using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_SettingPopup : UI_Popup
{
    enum GameObjects
    {
        Blocker,
    }
    protected override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        GetObject((int)GameObjects.Blocker).EventBind(ClosePopup);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    void ClosePopup(PointerEventData data)
    {
        Managers.UI.ClosePopupUI(this);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
