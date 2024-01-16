

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] private Volume globalVolume;
   
    private Vignette dof;
    private Tween _tween;
    private int CurrentSceneIndex;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindAnyObjectByType<GameManager> ();

            return instance;
        }
    }


    public UnityAction UIEnable;
    public UnityAction UIDisable;
    //public UnityAction<int> SceneLoad;

    private void Awake ()
    {
         PlayerCharacter.Instance.Ondeath += PlayerDie;
    }

    private void OnDestroy ()
    {
       Delegate[] d = UIEnable.GetInvocationList ();
        foreach(var a in d)
        {
            UIEnable -= a as UnityAction;
        }
        Delegate[] s = UIDisable.GetInvocationList ();
        foreach(var a in s )
        {
            UIDisable -= a as UnityAction;
        }

    }
    

    private void Start()
    {
        globalVolume.profile.TryGet<Vignette>(out dof);
       
       // UIDisable.Invoke();
        
    }

    private void PlayerDie()
    {
        UIEnable.Invoke();
        AudioManager.Instance.BGMStop();
    }
    
    public void BlackScreen()
    {
       dof?.smoothness.SetValue(new FloatParameter(1f));
     _tween =   DOVirtual.DelayedCall(0.2f, () =>
        {
           dof?.smoothness.SetValue(new FloatParameter(0.1f));
            
        }, false);
    }
  

    public void GameOver()
    {
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }


    public void ReStartGame()
    {
        CurrentSceneIndex = SceneManager.GetActiveScene ().buildIndex;
        ScenMonitoring.ChangeScene (CurrentSceneIndex);
    }
    

}
