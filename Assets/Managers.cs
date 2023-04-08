using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // s : static ?
    static Managers Instance { get { Init(); return s_instance; } }

    SoundManager _sound = new SoundManager();
    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    UIManager _ui = new UIManager();

    public static SoundManager Sound { get { return Instance._sound; } }
    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource;} }
    public static UIManager UI { get { return Instance._ui; } }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        
    }

    static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            // Init Other //
            s_instance._sound.Init();
        }
    }
}
