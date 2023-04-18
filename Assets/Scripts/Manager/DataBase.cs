using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB { }
[System.Serializable]
public class Chapter : DB
{
    public int _idx;
    public int _starTotal;
    public bool isOpen;
    // List<Stage> _stages;
}
[System.Serializable]
public class Stage : DB
{
    public int idx;
    public string name;
    public int getStar;
    public float percent;
    public bool repeat;
    public bool isOpen;
    public bool isClear;
    // public int clearGold
    public int first_Gold;
    public int repeat_Gold;
    public string bossName;
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
[System.Serializable]
public class UserInfo :DB
{
    public int lvHP;
    public int lvATK;
    public int lvSHIELD;
    public int atk;
    public int hp;
    public int shield;
    public int gold;
}
public class DataBase : MonoBehaviour
{
    static DataBase s_instance;
    static DataBase Instance { get { return s_instance; } }

    static void Init()
    {
        GameObject root = GameObject.Find("@DB");
        if (root == null)
        {
            root = new GameObject { name = "@DB" };
            root.AddComponent<DataBase>();
        }
        s_instance = root.GetComponent<DataBase>();
        DontDestroyOnLoad(root);
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
        if (LoadData() == false)
            Debug.LogError("Failed to Load Data");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    Dictionary<Type, List<DB>> _objects = new Dictionary<Type, List<DB>>();

    [SerializeField] List<Chapter> _chapters = new List<Chapter>();
    [SerializeField] List<Stage> _stages = new List<Stage>();
    [SerializeField] List<AtkEnhance> _atkEnhacne = new List<AtkEnhance>();
    [SerializeField] List<HpEnhance> _hpEnhacne = new List<HpEnhance>();
    [SerializeField] List<ShieldEnhance> _shieldEnhacne = new List<ShieldEnhance>();
    [SerializeField] List<Quest> _quest = new List<Quest>();
    [SerializeField] UserInfo _userInfo;// = null;
    bool LoadData()
    {
        var jsonChapterFile = Resources.Load<TextAsset>("Database/ChapterDB");
        _chapters = JsonConvert.DeserializeObject<List<Chapter>>(jsonChapterFile.ToString());
        Bind<Chapter>(_chapters);

        var jsonStageFile = Resources.Load<TextAsset>("Database/StageDB");
        _stages = JsonConvert.DeserializeObject<List<Stage>>(jsonStageFile.ToString());
        Bind<Stage>(_stages);

        var jsonAtkEnhanceFile = Resources.Load<TextAsset>("Database/AtkEnhanceDB");
        _atkEnhacne = JsonConvert.DeserializeObject< List<AtkEnhance>>(jsonAtkEnhanceFile.ToString());
        Bind<AtkEnhance>(_atkEnhacne);

        var jsonHpEnhanceFile = Resources.Load<TextAsset>("Database/HpEnhanceDB");
        _hpEnhacne = JsonConvert.DeserializeObject<List<HpEnhance>>(jsonHpEnhanceFile.ToString());
        Bind<HpEnhance>(_hpEnhacne);

        var jsonShieldEnhanceFile = Resources.Load<TextAsset>("Database/ShieldEnhanceDB");
        _shieldEnhacne = JsonConvert.DeserializeObject<List<ShieldEnhance>>(jsonShieldEnhanceFile.ToString());
        Bind<ShieldEnhance>(_shieldEnhacne);

        var jsonQuestDBFile = Resources.Load<TextAsset>("Database/QuestDB");
        _quest = JsonConvert.DeserializeObject<List<Quest>>(jsonQuestDBFile.ToString());
        Bind<Quest>(_quest);

        //string m_sSaveFileDirectory = Application.persistentDataPath;
        //Debug.Log(m_sSaveFileDirectory);
        //string jdata = File.ReadAllText("Assets/Resources/Database/UserDB.json");

        var jsonUserFile = Resources.Load<TextAsset>("Database/UserDB");
        _userInfo = JsonConvert.DeserializeObject<UserInfo>(jsonUserFile.ToString());

        return true;
    }
    public static void Save(Stage stage)
    {
        int idx = stage.idx;
        int chapter = idx / 5;
        if (chapter + 1 < s_instance._chapters.Count) // 다음 챕터가 있으면,
        {
            int total = 0;
            for (int i = 5 * chapter; i < 5 * chapter + 4; i++)
            {
                total += s_instance._stages[i].getStar;
            }

            if (10 <= total)
            {
                s_instance._chapters[chapter + 1].isOpen = true;
            }
        }
        if (s_instance._stages[idx + 1].isOpen == false)
            s_instance._stages[idx + 1].isOpen = true;
    }
    void Bind<T>(List<T> temp) where T: DB
    {
        List<DB> db = new List<DB>(new DB[temp.Count]);
        _objects.Add(typeof(T), db);
        for (int i = 0; i < temp.Count; i++)
        {
            db[i] = temp[i];
        }
    }
    // Chapter 가져오기 // 추후 수정 
    // Dictionary<Type, Object[]> ? 
    public static T Get<T>(int idx = 0) where T : DB
    {
        if(typeof(T) == typeof(UserInfo))
        {
            return s_instance._userInfo as T;
        }
        else
        {
            List<DB> db = new List<DB>();
            if (s_instance._objects.TryGetValue(typeof(T), out db) == false)
            {
                return null;
            }
            else
            {
                if (idx < db.Count)
                {
                    return db[idx] as T;
                }
                else
                {
                    Debug.Log("");
                    return null;
                }
            }
        }
    }
}
