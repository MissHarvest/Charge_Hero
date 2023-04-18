using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StageManager : MonoBehaviour
{
    // instacne 제거 //
    [SerializeField] GameObject player;
    [SerializeField] GameObject boss;
    PlayerStatus status;
    public Stage stage;
    static StageManager s_instance;
    public static StageManager Instance { get { return s_instance; } }
    // UI_Distance ui_Dist;
    [SerializeField] float maxDist;
    [SerializeField] float dist;
    float Dist { get { return dist; }  set { if (value >= maxDist) dist = maxDist; else dist = value; } }
    float Percent { get { return Dist / maxDist; } }
    // 구출한 동료 수
    public int rescue;

    // 보스 죽이기
    public bool kill;

    // 피격 횟수
    public int attacked_Cnt;
    
    public Quest[] quests;
    public int[] questidx;

    public void Set(GameObject player, GameObject boss)
    {
        this.player = player;
        this.boss = boss;
        maxDist = boss.transform.position.x - player.transform.position.x;
        
        status = player.GetComponent<PlayerStatus>();
        player.GetComponent<PlayerControl>().DieAction(CheckQuest);
    }
    private void Awake()
    {
        Debug.Log($"Stage Manager({this.gameObject.name}). Awake");
    }
    public void PlayerAttacked()
    {
        attacked_Cnt++;
    }
    void Init()
    {
        if(s_instance == null)
        {
            s_instance = this;
            stage = Managers.stage;

            string[] tmp = stage.questType.Split(',');
            questidx = System.Array.ConvertAll<string, int>(tmp, int.Parse);
            quests = new Quest[3];
            for (int i = 0; i < quests.Length; i++)
            {
                quests[i] = DataBase.Get<Quest>(questidx[i]);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    public void CheckQuest()
    {
        int clear_Cnt = 0;
        Dist = maxDist - (boss.transform.position.x - player.transform.position.x);
        for (int i = 0; i < quests.Length; i++)
        {
            switch (quests[i].type)
            {
                case E_Quest.Run:
                    Debug.Log($"Percent{Percent}");
                    if (Percent >= quests[i].value)
                    {
                        clear_Cnt++;
                        Debug.Log("Run Clear");
                    }
                    break;

                case E_Quest.HP: // Boss Kill 조건
                    float rat = (float)status.HP / (float)DataBase.Get<UserInfo>().hp;// instance.status.hp;
                    if (rat >= quests[i].value)
                    {
                        clear_Cnt++;
                        Debug.Log("HP Clear");
                    }
                    break;

                case E_Quest.Rescue:
                    if (rescue >= quests[i].value)
                    {
                        clear_Cnt++;
                        Debug.Log("Rescue Clear");
                    }
                    break;

                case E_Quest.Attacked:
                    if (kill)
                    {
                        if (attacked_Cnt <= quests[i].value)
                        {
                            clear_Cnt++;
                            Debug.Log("Attacked Clear");
                        }
                    }
                    break;

                case E_Quest.BOSS:
                    if (kill)
                    {
                        clear_Cnt++;
                        Debug.Log("BOSS Clear");
                    }
                    break;
            }
        }
        int reward_Gold = stage.repeat ? (int)(stage.repeat_Gold * dist/maxDist) : stage.first_Gold;
        DataBase.Get<UserInfo>().gold += reward_Gold;
        stage.Update_Info(clear_Cnt, Percent, kill);
        DataBase.Save(stage);
        if (!kill)
        {
            Managers.UI.ShowPopupUI<UI_Popup>("UI_GameOver");
        }
        StartCoroutine(ShowResult(stage));
    }
    IEnumerator ShowResult(Stage stage)
    {
        yield return new WaitForSeconds(1.0f);
        Managers.UI.ShowPopupUI<UI_Result>().Set(stage);
    }
}
