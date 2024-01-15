

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] private Volume globalVolume;
   
    private Vignette dof;
    private Tween _tween;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindAnyObjectByType<GameManager> ();
            return instance;
        }
    }

    public UnityAction InputEnable;
    public UnityAction InputDisable;

    private void Start()
    {
        globalVolume.profile.TryGet<Vignette>(out dof);
      
    }

    public void BlackScreen()
    {
       dof?.smoothness.SetValue(new FloatParameter(1f));
     _tween =   DOVirtual.DelayedCall(0.2f, () =>
        {
           dof?.smoothness.SetValue(new FloatParameter(0.1f));
            
        }, false);
    }
    private void Awake ()
    {
        DontDestroyOnLoad (gameObject);
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
        SceneManager.LoadSceneAsync("Scenes/Hollow Knight");
    }
    

}
