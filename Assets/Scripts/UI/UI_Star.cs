using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Star : UI_Base
{
    enum GameObjects
    {
        Star1,
        Star2,
        Star3,
    }
    int cnt;
    protected override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        for(int i = 0; i < 3; i++)
        {
            if (i < cnt)
                GetObject(i).SetActive(true);
            else
                GetObject(i).SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Set(int cnt)
    {
        this.cnt = cnt;
    }
}
