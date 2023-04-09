using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        // To do //
        Managers.UI.ShowPopupUI<UI_GameplayPanel>();
    }

    public override void Clear()
    {
        // throw new System.NotImplementedException();

    }
}
