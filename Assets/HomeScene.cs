using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Home;
    }
    private void Start()
    {
        Managers.UI.ShowSceneUI<UI_TitleScene>();
    }
    public override void Clear()
    {
        // throw new System.NotImplementedException();
    }
}
