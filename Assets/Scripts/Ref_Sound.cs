using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ref_Sound
{
    //    AudioSource[] _audioSources = new AudioSource[10]; // �뵵�� ����� ����� ����.
    //    // mp3 audiosource�� ���� �迭 ����

    //    // ĳ�� ������ �� _audioClips ��Ǿ 
    //    // ĳ���� ���带 ����Ҷ����� ��� path�� �Է¹޾� ���带 ã�� ����� �ƴ� �ѹ� ����� ����� ��ųʸ��� �����Ͽ� ���� ������ ó���ϱ� ���ؼ� ĳ�̿� ��ǳʸ��� ����ϴ� ���̴�.
    //    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    public void init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            //string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));

//            for (int i = 0; i < soundNames.Length - 1; i++)
//            {
//                GameObject go = new GameObject { name = soundNames[i] };
//                _audioSources[i] = go.AddComponent<AudioSource>(); // ������ ���� _audioSources�� �־��ش�.
//                go.transform.parent = root.transform;
//            }
//            // soundName�� ���鼭 ���ο� GameObject�� ������ش�.

//            _audioSources[(int)Define.Sound.Bgm].loop = true; // Bgm���� ��쿡�� ������ ��� ���尡 ������ ���ش�.
        }

    }

//    public void Clear()
//    {
//        foreach (AudioSource audioSource in _audioSources)
//        {
//            audioSource.clip = null;
//            audioSource.Stop();
//        }
//        _audioClips.Clear();
//    }

//    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f) // path�� ��θ� �޾��ְ� pitch = �Ҹ� �ӵ� ����
//    {
//        AudioClip audioclip = GetOrAddAudioClip(path, type);
//        Play(audioclip, type, pitch);
//    }

//    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f) // path�� ��θ� �޾��ְ� pitch = �Ҹ� �ӵ� ����
//    {
//        if (audioClip == null)
//        {
//            return;
//        }

//        if (type == Define.Sound.Bgm)
//        {
//            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
//            if (audioSource.isPlaying)
//                audioSource.Stop();

//            audioSource.pitch = pitch;
//            audioSource.clip = audioClip;
//            audioSource.Play();
//        }

//        else // (type == Define.Sound.Effect)
//        {
//            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
//            audioSource.pitch = pitch;
//            audioSource.clip = audioClip;
//            audioSource.PlayOneShot(audioClip);
//        }
//    }


//    AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect) // audioClip ��ȯ�ϴ� �Լ�(���� Dictionary���� �κп���)
//    {
//        if (path.Contains("Sounds/") == false)
//        {
//            path = $"Sounds/{path}";
//        }

//        AudioClip audioClip = null;

//        if (type == Define.Sound.Bgm)
//        {
//            audioClip = Managers.Resource.Load<AudioClip>(path);
//        }
//        else
//        {
//            if (_audioClips.TryGetValue(path, out audioClip) == false) // ������ �̷��� ���� ����ְ� 
//            {
//                audioClip = Managers.Resource.Load<AudioClip>(path);
//                _audioClips.Add(path, audioClip);
//            }
//        }

//        if (audioClip == null)
//        {
//            Debug.Log($"AudioClip Missing ! {path}");
//        }

//        return audioClip;
//    }

}