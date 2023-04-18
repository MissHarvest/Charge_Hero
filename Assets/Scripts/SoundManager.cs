using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections.Generic;

public enum SoundType { Bgm, Effect, MaxCount }
public class SoundManager// : MonoBehaviour
{
    AudioSource[] _audioSources = new AudioSource[(int)SoundType.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();
    
    enum BGMSound { TitleBGM, SceneBGM, PlayBGM, BossBGM }
    enum EffectSound { Click, Jump, GetCoin, Enhance, Attack, Attacked, Fail, Clear }


    // 제거 ->> //
    public static SoundManager instance;

    public Slider MasterSlider;
    public Slider BGM_Slider;
    public Slider EffectSlider;
    public AudioMixer audioMixer;

    // 초기 값 //
    public float masterVolume;
    public float bgmVolume;
    public float effectVolume;

    public AudioSource BG_AudioSource;
    public AudioSource Effect_AudioSource;
    // <<- 여기까지 // 

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if(root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundTypeNames = System.Enum.GetNames(typeof(SoundType));
            for(int i = 0; i < soundTypeNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundTypeNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }
            _audioSources[(int)SoundType.Bgm].loop = true;
        }
    }
    public void Clear()
    {
        foreach(AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }

        _audioClips.Clear();
    }
    public void Play(AudioClip audioClip, SoundType type = SoundType.Effect, float pitch = 1.0f)
    {
        if(audioClip == null)
        {
            Debug.LogError("AudioClip is null");
            return;
        }

        if(type == SoundType.Bgm)
        {
            AudioSource audioSource = _audioSources[(int)SoundType.Bgm];
            
            if(audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)SoundType.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }
    public void Play(string path, SoundType type = SoundType.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
        // Define.Scene d = Define.Scene.Home;
    }
    AudioClip GetOrAddAudioClip(string path, SoundType type = SoundType.Effect)
    {
        if(path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        AudioClip audioClip = null;

        if(type == SoundType.Bgm)
        {
            audioClip = Resources.Load<AudioClip>(path); // Managers.Resource.Load
        }
        else
        {
            if(_audioClips.TryGetValue(path,out audioClip) == false)
            {
                audioClip = Resources.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }

        if(audioClip == null)
        {
            Debug.Log($"AudioClip Missing! {path}");
        }

        return audioClip;
    }
    // 아래 제거 //
    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        Object.DontDestroyOnLoad(this.gameObject);
    //        AudioClip audioclip = null;

    //        // BGM AudioClip Set //
    //        string[] bgmNames = System.Enum.GetNames(typeof(BGMSound));            
    //        for (int i = 0; i < bgmNames.Length; i++)
    //        {
    //            string path = string.Format($"Sound/{bgmNames[i]}");
    //            audioclip = Resources.Load<AudioClip>(path);
    //            _audioClips.Add(bgmNames[i], audioclip);
    //        }
    //        BG_AudioSource.loop = true;

    //        // Effect AudioClip Set //
    //        string[] effectNames = System.Enum.GetNames(typeof(EffectSound));
    //        for (int i = 0; i < effectNames.Length; i++)
    //        {
    //            string path = string.Format($"Sound/{effectNames[i]}");
    //            audioclip = Resources.Load<AudioClip>(path);
    //            _audioClips.Add(effectNames[i], audioclip);
    //        }
    //    }
    //    else
    //        Destroy(this.gameObject);
    //}
    private void Start()
    {
        // Volume 초기값 설정 //
        // DB 에서 바로 읽기
        // 저장할 때 각 Slider.value 저장.
        //masterVolume = -20f;
        //bgmVolume = -20f;
        //effectVolume = -20f;

        //DBLoader.Instance.LoadSoundTest();
        MasterAudioControl();
        BGMAudioControl();
        EffectAudioControl();

        
    }
    public void PlayBGM(string name)
    {
        BG_AudioSource.Stop();
        AudioClip temp = null;
        _audioClips.TryGetValue(name, out temp);
        BG_AudioSource.clip = temp;
        BG_AudioSource.Play();
    }
    public void OffBGM()
    {
        _audioSources[(int)SoundType.Bgm].Stop();
    }    
    public void PlayEffect(string name)
    {
        //Debug.Log("Play[" + name + "]Effect");
        AudioClip temp = null;
        _audioClips.TryGetValue(name, out temp);
        //Effect_AudioSource.clip = temp;
        Effect_AudioSource.PlayOneShot(temp);
    }
    // Slider value 읽고 쓰기 // 
    public void MasterAudioControl()
    {   // 일단 Slide 별로 함수 만들고 나중에 병합.
        float sound = MasterSlider.value;
        if (sound == -40f) audioMixer.SetFloat("Master", -80f);
        else audioMixer.SetFloat("Master", sound);
        audioMixer.GetFloat("Master", out sound);
    }
    public void BGMAudioControl()
    {   // 일단 Slide 별로 함수 만들고 나중에 병합.
        float sound = BGM_Slider.value;

        if (sound == -40f) audioMixer.SetFloat("BGM", -80f);
        else audioMixer.SetFloat("BGM", sound);
    }
    public void EffectAudioControl()
    {   // 일단 Slide 별로 함수 만들고 나중에 병합.
        float sound = EffectSlider.value;

        if (sound == -40f) audioMixer.SetFloat("Effect", -80f);
        else audioMixer.SetFloat("Effect", sound);
    }
    public void ToggleAudioVolume()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }
}
