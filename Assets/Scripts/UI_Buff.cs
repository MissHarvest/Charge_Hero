using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Buff : UI_Base
{
    enum Images
    {
        Buff_Img,
    }
    enum Texts
    {
        Timer_Text,
    }
    PlayerEffect playerEffect;
    bool usable;
    string[] buffnames;
    public void Set(GameObject player)
    {
        this.playerEffect = player.GetComponent<PlayerEffect>();
    }
    protected override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));
        buffnames = System.Enum.GetNames(typeof(Define.Effects));
        this.gameObject.SetActive(false);
        playerEffect.buffTimerCount = OnBuffUI;

    }
    private void Start()
    {
        Init();
    }
    float buffTime = 5;
    float timer;
    // Update is called once per frame
    void Update()
    {
        TimerStart();
    }
    public void ResetTimer()
    {
        timer = buffTime;
    }
    void OnBuffUI(Define.Effects type)
    {
        GetImage((int)Images.Buff_Img).sprite = Managers.Resource.Load<Sprite>($"Images/{buffnames[(int)type]}");
        if (gameObject.activeSelf == false) gameObject.SetActive(true);
        timer = buffTime;
    }
    void TimerStart()
    {
        GetText((int)Texts.Timer_Text).text = timer.ToString("F1");

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            playerEffect.OffEffect();
            gameObject.SetActive(false);
        }
    }
}
