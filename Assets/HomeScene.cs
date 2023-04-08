using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Home;
        // Home Scene 을 가지고 있는 GameObject (@Scene) 의 Awake() 가 호출되면 실행할 내용 //

        // Managers.UI.ShowSceneUI<UI_Scene>();
        // Managers.UI.ShowPopupUI<UI_GameplayPanel>();
    }

    public override void Clear()
    {
        // throw new System.NotImplementedException();
    }
}
