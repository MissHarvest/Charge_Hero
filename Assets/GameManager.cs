using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public struct Status
{
    public int Lv_HP;
    public int Lv_ATK;
    public int Lv_DEF;
    public int hp;
    public int atk;
    public int def_cnt;
}
[Serializable]
public struct Enhance
{
    public int stat;
    public int need_Gold;
    public void Set(int stat, int gold)
    {
        this.stat = stat;
        this.need_Gold = gold;
    }
}
[Serializable]
public class Chapter
{
    public int stars;
    int need_Cnt = 10;
    public int total
    {
        get { stars = 0; for(int i = 0; i < stages.Count; i++)  stars += stages[i].getStar; return stars; }
    }
    public bool isEnough { get { return (total >= need_Cnt); } }
    public List<Stage> stages;
    public void Link_with(Stage stage)
    {
        if(stages == null) stages = new List<Stage>();
        stages.Add(stage);
        //Debug.Log("Link_with . . ." + stage.name);
    }
}

// chapter cnt �� ���� ���� Define
[Serializable]
public class Stage
{
    public int idx;
    public string name;
    public int getStar;
    public float percent;
    public bool repeat;
    public bool isClear;
    // public int clearGold
    public int first_Gold;
    public int repeat_Gold;
    public int bossHP;
    public string questType;
    public void Update_Info(int cnt, float percent, bool kill)
    {
        repeat = true;
        if (cnt > getStar) this.getStar = cnt;
        if (isClear != kill) isClear = true;
        if (kill) this.percent = 1;
        else if (percent > this.percent) this.percent = percent;     
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //static GameManager Instance { get { return instance; } }
    [Header("�ٷΰ���")]
    public followCamera followCam;
    public GameObject player_prefab;
    public GameObject go_Player;
    public GameObject go_Boss;
    //static StageManager _sm;
    //public static StageManager SM { get { return GameManager._sm; } }
    //public BuffManager buffManager;

    [Header("�÷��̾� �⺻ �ɷ�ġ")]
    public Status status;
    public int n_Gold;
    public bool changeCoin;
    
    [ReadOnly("ReadOnly_Text")]         // �ν����Ϳ��� ������ �� ���� ��
    public int readOnly;                    // string �� ������ ������ ǥ���� �ؽ�Ʈ

    [Label("Label")]
    public int label;

    [HorizontalLine]
    public int horizontal;

    public Enhance[] en_HP;
    public Enhance[] en_ATK;
    public Enhance[] en_DEF;
    // ��ȭ�� ������ > �ɷ�ġ ���� Ȯ���Ѵ� > �÷��̾��� �ش� �ɷ�ġ Lv �� ȣ���Ѵ�.
    // Lv(idx) ���� ���� ���(gold) �� ��ġ(stat)�� �����´�.

    [Header("��")]
    public Chapter[] chapters;
    public Stage[] stages;  // List ����
    public int ply_Chapter; // clear Chapter?
    public int ply_Stage; // clear Stage?
    public int chapter;
    public int stage;
    //public UI_Distance ui_dist;

    public void SetResolutions()
    {
        int setWidth = 3200; // ����� ���� �ʺ�
        int setHeight = 1440; // ����� ���� ����

        int deviceWidth = Screen.width; // ��� �ʺ� ����
        int deviceHeight = Screen.height; // ��� ���� ����

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true);
        // SetResolution �Լ� ����� ����ϱ�

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // ����� �ػ� �� �� ū ���
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // ���ο� �ʺ�
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // ���ο� Rect ����
        }
        else // ������ �ػ� �� �� ū ���
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // ���ο� ����
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // ���ο� Rect ����
        }
    }

    public string map
    {
        get { return string.Format($"{chapter}-{stage}"); }
    }

    // inline �Լ� //
    public Func<int, int, int> GetIndex = (ch, stg) => ((ch - 1) * 5) + (stg - 1);
    //public static Func<int, int, int> TestFunc = (ch, stg) => ((ch - 1) * 5) + (stg - 1);

    // Start is called before the first frame update
    void Awake()
    {
        //SetResolutions();
        if (instance == null)
        {
            Debug.Log("GameManager_Awake");
            instance = this;

            //DBLoader.Instance.LoadTest();
            player_prefab = Resources.Load<GameObject>("Prefabs/Player");

            SceneManager.sceneLoaded += LoadScene;
            //SceneManager.sceneUnloaded += UnLoadScene;

            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }
    
    void Init_StageInfo()
    {
        stages = DBLoader.Instance.stagedb;

        chapters = new Chapter[1];//3
        for(int i =0; i < chapters.Length; i++)
        {
            chapters[i] = new Chapter();
            for (int j = 0; j < 5; j++)
            {
                chapters[i].Link_with(stages[i * 5 + j]);
            }
        }
    }
    void Init_EnhanceInfo()
    {
        en_HP = DBLoader.Instance.hpenhance;
        en_ATK = DBLoader.Instance.atkenhance;
        en_DEF = DBLoader.Instance.defenhance;
    }
    public Enhance GetEnhanceInfo(E_Status type)
    {
        Enhance en = new Enhance();
        en.Set(0, 0);
        switch (type)
        {
            case E_Status.HP:
                if(status.Lv_HP < en_HP.Length)
                    en = en_HP[status.Lv_HP];
                break;
            case E_Status.ATK:
                if (status.Lv_ATK < en_ATK.Length)
                    en = en_ATK[status.Lv_ATK];
                break;
            case E_Status.DEF:
                if (status.Lv_DEF < en_DEF.Length)
                    en = en_DEF[status.Lv_DEF];
                break;
        }
        return en;
    }
    public string GetStatusInfo(E_Status type)
    {
        string info = null;
        switch (type)
        {
            case E_Status.HP:
                info = string.Format($"Lv.{status.Lv_HP} HP {status.hp}");
                break;
            case E_Status.ATK:
                info = string.Format($"Lv.{status.Lv_ATK} ATK {status.atk}");
                break;
            case E_Status.DEF:
                info = string.Format($"Lv.{status.Lv_DEF} DEF {status.def_cnt}");
                break;
        }
        return info;
    }
    private void Start()
    {
        Debug.Log("GameManager_Start");
        Init_StageInfo();
        Init_EnhanceInfo();

        DBLoader.Instance.LoadTest();
    }
    public void GetChapter(int _chapter)
    {
        chapter = _chapter;
    }
    public void GetStage(int _stage)
    {
        stage = _stage;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DBLoader.Instance.SaveTest();
            Application.Quit();
        }
    }
    public void LoadScene(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.name + "�� Load �մϴ�.");
        switch (scene.name)//Load �ϴ� Scene �̸�.
        {
            case "UI":
                //plusStat.Init();
                break;
            default:
                if (scene.name == "1-5")
                {
                    SoundManager.instance.PlayBGM("BossBGM");
                }
                else
                {
                    SoundManager.instance.PlayBGM("PlayBGM");
                }
                SetPlayer();// ���� ���� �Ҵ� �ؾ��ұ�? //
                StageManager.instance.Set();
                break;
        }
    }
    public void SetPlayer()
    {
        // Player Prefab ���� �� ��ġ.
        go_Player = Instantiate(player_prefab);
        Debug.Log("Creat Player in " + SceneManager.GetActiveScene().name);
        followCam.go_Player = go_Player;
        go_Player.GetComponent<PlayerControl>().cam = followCam;

        // ���׷��̵� �� �ɷ�ġ Prefab�� ����
        go_Player.GetComponent<PlayerStatus>().InitStatus(status);
    }
    public void GameEnd()
    {
        StageManager.instance.CheckQuest();
        OpenStage();
        DBLoader.Instance.SaveTest();
    }
    void OpenStage()
    {
        if (stage < 5)
        {
            stage++;
            if (ply_Chapter == chapter && ply_Stage < stage) ply_Stage = stage;
        }
        if (chapters[ply_Chapter - 1].isEnough)
        {
            ply_Chapter++;
            ply_Stage = 1;
            chapter = ply_Chapter;
        }
    }
    public Stage GetStageData()
    {
        // map(string ) �� int������ �����ؼ� �ѱ��.
        string[] data = map.Split('-');
        int chap = int.Parse(data[0]);
        int stage = int.Parse(data[1]);
        return stages[GetIndex(chap, stage)];
    }
}