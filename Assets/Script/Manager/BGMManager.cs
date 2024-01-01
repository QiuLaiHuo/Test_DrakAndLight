using UnityEngine;

public class BGMManager: MonoBehaviour
{
    private static BGMManager instance;
    public static BGMManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindAnyObjectByType<BGMManager> ();
            return instance;
        }
    }
    private AudioSource audiosource;
    private int currentClip;

    public AudioClip[] clips;


    private void Start ()
    {
        currentClip = 0;
        audiosource = GetComponent<AudioSource> ();

        audiosource.clip = clips[currentClip];
        audiosource.Play ();
    }

    private void Update ()
    {
        if (!audiosource.isPlaying)
        {
            currentClip++;
            ClipCheck ();

            audiosource.clip = clips[currentClip];
            audiosource.Play ();

        }
    }

    private void ClipCheck ()
    {
        int len = clips.Length;
        if (currentClip == len )
        {
            currentClip = 0;
        }

    }
}
