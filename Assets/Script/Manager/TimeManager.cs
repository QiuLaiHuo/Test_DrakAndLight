using System.Collections;

using UnityEngine;

public class TimeManager: MonoBehaviour
{

    private static TimeManager instance;
    public static TimeManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindAnyObjectByType<TimeManager> ();
            return instance;
        }
    }


    [Range (0f,1f)] public float SlowDuration;

    private bool IsSlow = false;
    private float CurrentSlowTime = 0;

    void Update ()
    {
        if (CurrentSlowTime > 0 && !IsSlow)
        {
            StartCoroutine (Slow ());
        }

    }

    public void SlowTime ()
    {

        CurrentSlowTime = SlowDuration;
    }

    IEnumerator Slow ()
    {
        IsSlow = true;
        var t = Time.timeScale;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime (SlowDuration);
        Time.timeScale = t;
        CurrentSlowTime = 0;
        IsSlow = false;
    }
}
