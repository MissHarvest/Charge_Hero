using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_List : UI_Base
{
    int max;
    Define.E_Status type;
    List<GameObject> go_list = new List<GameObject>();

    protected override void Init()
    {
        switch(type)
        {
            case Define.E_Status.HP:
                max = status.HP;
                break;
            case Define.E_Status.SHIELD:
                max = status.SHIELD;
                break;
        }
        string[] types = System.Enum.GetNames(typeof(Define.E_Status));
        int idx = (int)type;
        GameObject prefab;
        for(int i = 0; i < max; i++)
        {
            prefab = Managers.Resource.Instantiate($"UI/SubUI/{types[idx]}");
            prefab.transform.SetParent(this.gameObject.transform);
            go_list.Add(prefab);
        }
    }
    private void Start()
    {
        Init();
    }
    [SerializeField] PlayerStatus status;
    public void Set(GameObject player, Define.E_Status type)
    {
        this.status = player.GetComponent<PlayerStatus>();
        status.HpChanged(_update);

        this.type = type;
    }
    void _update()
    {
        int cur = 0;
        switch(type)
        {
            case Define.E_Status.HP:
                cur = status.HP;
                break;
            case Define.E_Status.SHIELD:
                cur = status.SHIELD;
                break;
        }

        for(int i = 0; i < max; i++)
        {
            if (i < cur)
                go_list[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            else
                go_list[i].GetComponent<Image>().color = new Color(0, 0, 0, 1);
        }
    }
}
