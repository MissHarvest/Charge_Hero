using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BackGround : UI_Popup
{
    enum GameObjects
    {
        BackGround,
    }

    protected override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        gameObject.GetComponent<Canvas>().sortingOrder = -1;
        clone = Instantiate(GetObject((int)GameObjects.BackGround), transform);
        cloneRect = clone.GetComponent<RectTransform>();//.
        Debug.Log(Screen.width);
        cloneRect.transform.localPosition = new Vector3(Screen.width-10, 0);
        backRect = GetObject((int)GameObjects.BackGround).GetComponent<RectTransform>();
        backRect.transform.localPosition = Vector3.zero;
        speed = 500f;
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    RectTransform backRect;
    RectTransform cloneRect;
    GameObject clone;
    float speed;
    // Update is called once per frame
    void Update()
    {
        backRect.transform.localPosition += Vector3.left * speed * Time.deltaTime;
        if (backRect.transform.localPosition.x <= -Screen.width)
            backRect.transform.localPosition = new Vector3(Screen.width - 10, 0, 0);

        cloneRect.transform.localPosition += Vector3.left * speed * Time.deltaTime;
        if (cloneRect.transform.localPosition.x <= -Screen.width)
            cloneRect.transform.localPosition = new Vector3(Screen.width - 10, 0, 0);
    }
    private void OnDestroy()
    {
        Managers.UI.ClosePopupUI();
    }
}
