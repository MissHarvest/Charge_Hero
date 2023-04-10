using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : BaseScene
{
    GameObject player;
    GameObject boss;
    //GameObject _camera;
    UI_GameplayPanel gamepanel;
    followCamera _camera;
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;
    }
    private void Start()
    {
        // To do //
        Debug.Log("ShowSceneUI<UI_GameplayPanel>");
        gamepanel = Managers.UI.ShowSceneUI<UI_GameplayPanel>();

        // 캐릭터와 점프 및 슬라이드 Btn 을 연동할 필요가 있음.
        player = Managers.Resource.Instantiate("Player");
        player.GetComponent<PlayerStatus>().Set(DataBase.Get<UserInfo>());

        // Boss 생성 // Stage 는 보스 이름도 가지고 있어야 할듯 //
        // 임시 //
        boss = GameObject.Find("Frog");

        gamepanel.Set(player, boss);

        // Camera 가 Player 를 참조 //
        _camera = GameObject.Find("Main Camera").GetComponent<followCamera>();
        if (_camera == null)
        {
            GameObject _go = new GameObject { name = "Main Camera" };
            _go.GetOrAddComponet<Camera>();
            _camera = _go.GetOrAddComponet<followCamera>();
        }
        _camera.Set(player);
        

        GameObject go = GameObject.Find("@Stage");
        if (go == null)
        {
            go = new GameObject { name = "@Stage" };
        }
        StageManager stg = go.GetOrAddComponet<StageManager>();
        
        
    }
    public override void Clear()
    {
        // throw new System.NotImplementedException();

    }
}
