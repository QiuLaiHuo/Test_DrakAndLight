using System;
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

        [Header("Type")] 
        public AudioType type;
            
        [Header("音乐片段")]
        public AudioClip clip;
        
        [Header("分组")]
        public AudioMixerGroup group;

        [Header("音量")] 
        [Range(0,1)]
        public float volume=1f;
        
        [Header("启动循环")] 
        public bool loop = false;
        //
        // [Header("自动播放")] 
        // public bool PlayOnAwake = false;
    }
    

    #endregion
 
    public List<Sound> sounds;

    private Dictionary<AudioType, AudioSource> AudioDic;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        AudioDic ??= new Dictionary<AudioType, AudioSource>();
    }


    private void Start()
    {
        foreach (Sound sound in sounds)
        {
            Debug.Log(sounds.Count);
            GameObject obj = new GameObject(sound.clip.name);
            obj.transform.SetParent(transform);
            AudioSource source = obj.AddComponent<AudioSource>();

            source.clip = sound.clip;
            source.loop = sound.loop;
            source.volume = sound.volume;
            // source.playOnAwake = sound.PlayOnAwake;
            source.outputAudioMixerGroup = sound.group;

            AudioDic.Add(sound.type,source);
            
        }
    }


    public void AudioPlay(AudioType audioType)
    {
        if(!AudioDic.ContainsKey(audioType))
            return;
        AudioDic[audioType].Play();
        
    }

    public void AudioStop(AudioType audioType)
    {
        if(!AudioDic.ContainsKey(audioType))
            return;
        AudioDic[audioType].Stop();
    }
}
