using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Result : UI_Popup
{
    enum Buttons
    {
        Menu_Btn,
        Stage_Btn,
        Next_Btn,
    }
    enum Texts
    {
        Quest_Text,
        Gold_Text,
    }
    protected override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        // Bind<GameObject>(typeof(GameObjects));

        GetButton((int)Buttons.Next_Btn).gameObject.SetActive(stage.isClear);
        GetButton((int)Buttons.Next_Btn).enabled = false; // юс╫ц //

        GetButton((int)Buttons.Menu_Btn).onClick.AddListener(() => 
                                {
                                    SceneManager.LoadScene("UI");
                                    //Managers.UI.ClosePopupUI(); 
                                    //Managers.UI.ShowSceneUI<UI_TitleScene>(); 
                                });
        GetButton((int)Buttons.Stage_Btn).onClick.AddListener(() => 
                                {
                                    Managers.UI.ClosePopupUI(this);
                                    Managers.UI.ClosePopupUI();
                                    Managers.UI.ShowSceneUI<UI_StageScene>(); 
                                } );

        GetText((int)Texts.Quest_Text).text = "";
        string[] tmp = stage.questType.Split(',');
        int[] questidx = System.Array.ConvertAll<string, int>(tmp, int.Parse);
        Quest[] quests = new Quest[3];
        for (int i = 0; i < quests.Length; i++)
        {
            quests[i] = DataBase.Get<Quest>(questidx[i]);
        }
        for (int i = 0; i < 3; i++)
        {
            GetText((int)Texts.Quest_Text).text += string.Format($"{i + 1}_ { quests[i].title}\n");
        }

        UI_Star stars = Managers.UI.MakeSubUI<UI_Star>(transform);
        stars.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        stars.Set(stage.getStar);

        reward = stage.repeat ? (int)(stage.repeat_Gold * stage.percent) : stage.first_Gold;
        GetText((int)Texts.Gold_Text).text = string.Format("x {0:#,##0}", reward);
    }
    Stage stage;
    int reward;
    private void Start()
    {
        Init();
    }
    public void Set(Stage stage)
    {
        this.stage = stage;
    }
}
