using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Gold : UI_Base
{
    enum Texts
    {
        Gold_Text,
    }
    protected override void Init()
    {
        Bind<Text>(typeof(Texts));

        GetText((int)Texts.Gold_Text).text = string.Format("{0:#,##0} G", DataBase.Get<UserInfo>().gold);

        for(int i = 0; i < enhanceUIs.Count; i++)
        {
            enhanceUIs[i].SetAction(Consume);
        }
    }
    public int consume;
    public List<UI_Enhance> enhanceUIs = new List<UI_Enhance>();
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Gold UI Start");
        Init();
    }
    public void Consume(int val)
    {
        consume = val;
        int haveGold = DataBase.Get<UserInfo>().gold;
        int afterGold = haveGold - consume;
        StartCoroutine(NumberAnimation(GetText((int)Texts.Gold_Text), afterGold, haveGold, "{0:#,##0} G"));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator NumberAnimation(Text text, float target, float current, string format)
    {
        float duration = 0.5f;// f_delay; // 카운팅에 걸리는 시간 설정. 

        float offset = (target - current) / duration; // 
        
        while (current > target)
        {
            current += offset * Time.deltaTime;
            text.text = string.Format(format, (int)current);
            yield return null;
        }
        current = target;
        text.text = string.Format(format, (int)current);
    }
}
