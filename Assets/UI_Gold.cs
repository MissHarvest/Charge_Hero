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
        // Debug.Log("UI_Gold Init");
        Bind<Text>(typeof(Texts));

        GetText((int)Texts.Gold_Text).text = string.Format("{0:#,##0} G", DataBase.Get<UserInfo>().gold);

        // Debug.Log();
        for(int i = 0; i < enhanceUIs.Count; i++)
        {
            enhanceUIs[i].SetAction(Consume);
        }
    }
    public void InitE()
    {
        Init();// 보호 수준 지랄할줄 알았는데
    }
    public int consume;
    public List<UI_Enhance> enhanceUIs = new List<UI_Enhance>();
    // Start is called before the first frame update
    void Start()
    {

    }
    public void Consume(int val)
    {
        consume = val;
        int haveGold = DataBase.Get<UserInfo>().gold;
        int afterGold = haveGold - consume;
        StartCoroutine(NumberAnimation(afterGold, haveGold, E_VALUE.GOLD));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator NumberAnimation(float target, float current, E_VALUE type)
    {
        float duration = 0.5f;// f_delay; // 카운팅에 걸리는 시간 설정. 

        float offset = (target - current) / duration; // 
        yield return null;
        while (current > target)
        {
            current += offset * Time.deltaTime;
            switch (type)
            {
                case E_VALUE.GOLD:
                    GetText((int)Texts.Gold_Text).text = string.Format("{0:#,##0} G", (int)current);
                    break;
                case E_VALUE.ATK:
                    //txt_Atk.text = string.Format("{0:#,##0}", (int)current);
                    break;
            }
            yield return null;
        }
        current = target;
        //Debug.Log("Current/Target" + current + "/" + target);
        switch (type)
        {
            case E_VALUE.GOLD:
                GetText((int)Texts.Gold_Text).text = string.Format("{0:#,##0} G", (int)current);
                break;
            case E_VALUE.ATK:
                //txt_Atk.text = string.Format("{0:#,##0}", (int)current);
                break;
        }
    }
}
