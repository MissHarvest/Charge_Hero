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
        Managers.UI.ShowSceneUI<UI_TitleScene>();

        // DB 불러오기 //
        // 클리어한 챕터 및 스테이지 번호
        // 볼륨 값
    }

    public override void Clear()
    {
        // throw new System.NotImplementedException();
    }
}
