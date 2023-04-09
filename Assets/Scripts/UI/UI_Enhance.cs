using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Enhance : UI_Base
{
    enum Texts
    {
        Status_Text,
        Gold_Text,
        Enhance_Text
    }
    enum Images
    { 
        Status_Img,
    }
    enum GameObjects
    {
        Blocker,
    }
    enum Buttons
    {
        Enhance_Btn,
    }
    string _name;
    [SerializeField] Define.E_Status _type;
    UserInfo userinfo;
    [SerializeField] Enhance enhance;
    bool maximum;
    protected override void Init()
    {
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        GetImage((int)Images.Status_Img).sprite = Managers.Resource.Load<Sprite>($"Images/{_name}");

        GetObject((int)GameObjects.Blocker).SetActive(false);
        userinfo = DataBase.GetUserInfo();
        GetButton((int)Buttons.Enhance_Btn).gameObject.EventBind(Enhance);
        // GetButton((int)Buttons.Enhance_Btn).gameObject.EventBind();

        UpdateEnhanceInfo();
    }
    public void SetInfo(string name, int type)
    {
        this._name = name;
        this._type = (Define.E_Status)type;
        gameObject.name = name;
    }
    void UpdateEnhanceInfo()
    {
        switch (_type)
        {
            case Define.E_Status.HP:
                GetText((int)Texts.Status_Text).text = $"Lv.{userinfo.lvHP}     {userinfo.hp}";
                enhance = DataBase.Get<HpEnhance>(userinfo.lvHP);
                break;
            case Define.E_Status.ATK:
                GetText((int)Texts.Status_Text).text = $"Lv.{userinfo.lvATK}     {userinfo.atk}";
                enhance = DataBase.Get<AtkEnhance>(userinfo.lvATK);
                break;
            case Define.E_Status.SHIELD:
                GetText((int)Texts.Status_Text).text = $"Lv.{userinfo.lvSHIELD}     {userinfo.shield}";
                enhance = DataBase.Get<ShieldEnhance>(userinfo.lvSHIELD);
                break;
        }
        GetText((int)Texts.Gold_Text).text = string.Format("{0:#,##0}", enhance.need_Gold);
        string[] typenames = System.Enum.GetNames(typeof(Define.E_Status));//) typeof(Define.E_Status))
        GetText((int)Texts.Enhance_Text).text = $"бу {enhance.stat} {typenames[(int)_type]}";
    }
    // = new AtkEnhance();
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Action<int> intAction; 
    public void SetAction(Action<int> action)
    {
        intAction -= action;
        intAction += action;
    }
    public void Enhance(PointerEventData data)
    {
        if (!maximum)
        {
            if (userinfo.gold >= enhance.need_Gold)
            {
                intAction.Invoke(enhance.need_Gold);
                userinfo.gold -= enhance.need_Gold;
                switch (_type)
                {
                    case Define.E_Status.HP:
                        userinfo.lvHP++;
                        userinfo.hp += enhance.stat;
                        enhance = DataBase.Get<HpEnhance>(userinfo.lvHP);
                        break;
                    case Define.E_Status.ATK:
                        userinfo.lvATK++;
                        userinfo.atk += enhance.stat;
                        enhance = DataBase.Get<AtkEnhance>(userinfo.lvATK);
                        break;
                    case Define.E_Status.SHIELD:
                        userinfo.lvSHIELD++;
                        userinfo.shield += enhance.stat;
                        enhance = DataBase.Get<ShieldEnhance>(userinfo.lvSHIELD);
                        break;
                }
                UpdateEnhanceInfo();

                if (enhance == null)
                {
                    maximum = true;
                    GetObject((int)GameObjects.Blocker).SetActive(true);
                }
            }
        }
    }
}
