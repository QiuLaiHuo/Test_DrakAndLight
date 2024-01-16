using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Audio;

public class AudioManager: MonoBehaviour
{

    #region  单例函数

    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindAnyObjectByType<AudioManager> ();

            return instance;
        }
    }

    #endregion


    #region 存储音乐信息类
    [Serializable]
    public class Sound
    {

        [Header ("Type")]
        public AudioType type;

        [Header ("音乐片段")]
        public AudioClip clip;

        [Header ("分组")]
        public AudioMixerGroup group;

        [Header ("音量")]
        [Range (0,1)]
        public float volume = 1f;

        [Header ("启动循环")]
        public bool loop = false;
        //
        // [Header("自动播放")] 
        // public bool PlayOnAwake = false;
    }


    #endregion

    public List<Sound> sounds;

    private Dictionary<AudioType,AudioSource> AudioDic;

    private void Awake ()
    {
      

        AudioDic ??= new Dictionary<AudioType,AudioSource> ();
    }


    private void Start ()
    {
        foreach (Sound sound in sounds)
        {
            Debug.Log (sounds.Count);
            GameObject obj = new GameObject (sound.clip.name);
            obj.transform.SetParent (transform);
            AudioSource source = obj.AddComponent<AudioSource> ();

            source.clip = sound.clip;
            source.loop = sound.loop;
            source.volume = sound.volume;
            // source.playOnAwake = sound.PlayOnAwake;
            source.outputAudioMixerGroup = sound.group;

            AudioDic.Add (sound.type,source);

        }
    }

    public void BGMStop()
    {
        foreach (var key in AudioDic.Keys)
        {
            AudioDic[key].Stop();
        }
    }
    
    public void AudioPlay(AudioType audioType)
    {
        if (!AudioDic.ContainsKey (audioType))
            return;

        AudioDic[audioType].Play ();
    }

    public void AudioPlay (AudioType audioType,float WaitTime)
    {
        if (!AudioDic.ContainsKey (audioType))
            return;

        StartCoroutine (Wait (audioType,WaitTime));
          
       
    }

    IEnumerator Wait(AudioType audioType,float wait)
    {
        yield return new WaitForSecondsRealtime (wait);
        AudioDic[audioType].Play ();
    }

    public void AudioStop (AudioType audioType)
    {
        if (!AudioDic.ContainsKey (audioType))
            return;
        AudioDic[audioType].Stop ();
    }

    public void AudioStop(AudioType audioType,bool Stop)
    {
        if (Stop)
            StopCoroutine (Wait (audioType,0));
        else
            return;
    }
}
