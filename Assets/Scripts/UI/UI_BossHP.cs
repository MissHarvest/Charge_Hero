using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BossHP : UI_Base
{
    enum Images
    {
        HP,
        Boss_UI,
    }
    enum Texts
    {
        HP_Text,
    }
    GameObject boss;
    BossMonster bossMonster;
    RectTransform hpUIRect; //playerUIRect
    float maxHP;
    float maxWidth;

    protected override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));

        hpUIRect = GetImage((int)Images.HP).gameObject.GetComponent<RectTransform>();
        maxWidth = hpUIRect.sizeDelta.x;
        Reset(this.boss);
    }
    private void Start()
    {
        Init();
        this.gameObject.SetActive(false);
    }
    public void Set(GameObject boss)
    {
        this.boss = boss;
    }
    private void Reset(GameObject boss)
    {
        // boss 정보 초기화 //
        this.boss = boss;
        bossMonster = boss.GetOrAddComponet<BossMonster>();
        maxHP = bossMonster.HP;

        // HP UI 초기화 //
        hpUIRect.sizeDelta = new Vector2(maxWidth, hpUIRect.sizeDelta.y);
        GetImage((int)Images.Boss_UI).sprite = Managers.Resource.Load<Sprite>($"Images/Boss/{boss.name}");
        GetText((int)Texts.HP_Text).text = string.Format("{0:F0} / {1:F0}", maxHP, maxHP);
        bossMonster.UIEnable = _update;
    }
    void _update(float target)
    {
        this.gameObject.SetActive(true);
        if (target == 0) target = maxHP;
        StartCoroutine(HPUI(target));
    }
    public IEnumerator HPUI(float target)
    {
        float duration = 0.5f; // 카운팅에 걸리는 시간 설정. 
        float current = maxHP;
        float width = hpUIRect.sizeDelta.x;

        float offset = (target - current) / duration; // 
        while (current > target)
        {
            current += offset * Time.deltaTime;
            float rat = current / maxHP;
            if (current <= 0) current = 0;
            else if (current < target) current = target;
            GetText((int)Texts.HP_Text).text = string.Format("{0:F0} / {1:F0}", current, maxHP);
            hpUIRect.sizeDelta = new Vector2(width * rat, hpUIRect.sizeDelta.y);
            yield return null;
        }
    }
}
