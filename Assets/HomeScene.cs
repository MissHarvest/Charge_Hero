using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Home;
        // Home Scene �� ������ �ִ� GameObject (@Scene) �� Awake() �� ȣ��Ǹ� ������ ���� //

        // Managers.UI.ShowSceneUI<UI_Scene>();
        // Managers.UI.ShowPopupUI<UI_GameplayPanel>();
    }

    public override void Clear()
    {
        // throw new System.NotImplementedException();
    }
}
