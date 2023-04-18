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

        // ĳ���Ϳ� ���� �� �����̵� Btn �� ������ �ʿ䰡 ����.
        player = Managers.Resource.Instantiate("Player");
        // Boss ���� // Stage �� ���� �̸��� ������ �־�� �ҵ� //
        // �ӽ� //
        boss = GameObject.Find($"{Managers.stage.bossName}");
        if (boss == null)
            Debug.Log($"Failed to find {Managers.stage.bossName}");
        boss.GetOrAddComponet<BossMonster>().HP = Managers.stage.bossHP;
        // Camera �� Player �� ���� //
        _camera = GameObject.Find("Main Camera").GetComponent<followCamera>();
        if (_camera == null)
        {
            GameObject _go = new GameObject { name = "Main Camera" };
            _go.GetOrAddComponet<Camera>();
            _camera = _go.GetOrAddComponet<followCamera>();
        }
        
        GameObject go = GameObject.Find("@Stage");
        if (go == null)
        {
            go = new GameObject { name = "@Stage" };
        }
        go.GetOrAddComponet<StageManager>().Set(player, boss);
        Debug.Log("Creat Stage Manager");
        gamepanel.Set(player, boss);
        player.GetComponent<PlayerStatus>().Set(DataBase.Get<UserInfo>());
        player.GetComponent<PlayerControl>().Set(boss, _camera);
        _camera.Set(player);
    }
    public override void Clear()
    {
        // throw new System.NotImplementedException();

    }
}
