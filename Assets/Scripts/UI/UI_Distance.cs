using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Distance : UI_Base
{
    enum Images
    {
        Player_Img,
        Boss_Img,
        Dist_Img,
    }

    [SerializeField] GameObject player;
    [SerializeField] GameObject boss;
    RectTransform playerUIRect;
    RectTransform distUIRect;

    float multy;
    float max_Dist;
    float move_Dist;

    public float percent { get { return move_Dist / max_Dist; } }

    protected override void Init()
    {
        Bind<Image>(typeof(Images));

        playerUIRect = GetImage((int)Images.Player_Img).GetComponent<RectTransform>();
        distUIRect = GetImage((int)Images.Dist_Img).GetComponent<RectTransform>();
        Reset(this.boss); //m_AnchoredPosition.x
    }

    // bool ready;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    public void Set(GameObject player, GameObject boss)
    {
        Debug.Log($"(UI_Distance)Recieved {player.name} , {boss.name}");
        this.player = player;
        this.boss = boss;
    }
    private void Reset(GameObject _boss)
    {
        Debug.Log($"(UI_Distance)Call Reset {boss.name}");
        move_Dist = 0;
        if (this.boss != _boss) this.boss = _boss;
        playerUIRect.anchoredPosition = new Vector2(0, playerUIRect.anchoredPosition.y);
        distUIRect.sizeDelta = new Vector2(0, distUIRect.sizeDelta.y);

        GetImage((int)Images.Boss_Img).sprite = Managers.Resource.Load<Sprite>($"Images/Boss/{this.boss.name}");
        max_Dist = boss.transform.position.x - player.transform.position.x;
        multy = 2000 / max_Dist;
    }
    // Update is called once per frame
    void Update()
    {
        float dist = boss.transform.position.x - player.transform.position.x;
        move_Dist = max_Dist - dist;

        distUIRect.sizeDelta = new Vector2(move_Dist * multy + 50, distUIRect.sizeDelta.y);
        if (distUIRect.sizeDelta.x >= 2000)
        {
            distUIRect.sizeDelta = new Vector2(2000, distUIRect.sizeDelta.y);
        }

        playerUIRect.anchoredPosition = new Vector2(move_Dist * multy, playerUIRect.anchoredPosition.y);
        if (playerUIRect.anchoredPosition.x >= 1800)
        {
            playerUIRect.anchoredPosition = new Vector2(1800, playerUIRect.anchoredPosition.y);
            return;
        }
    }
}
