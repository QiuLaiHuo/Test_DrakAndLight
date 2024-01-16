

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
    public static GameManager Instance => instance;


    public UnityAction UIEnable;
    public UnityAction UIDisable;

    
    
    private void Start()
    {
        globalVolume.profile.TryGet<Vignette>(out dof);
        PlayerCharacter.Instance.Ondeath += PlayerDie;
        UIDisable.Invoke();
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
    private void Awake ()
    {
       
        if(instance!=null)
            Destroy(gameObject);
        else if (instance==null)
        {instance = this;
            DontDestroyOnLoad (this);
        }
        
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
        
        SceneManager.LoadSceneAsync("Scenes/Hollow Knight",LoadSceneMode.Single);
    }
    

}
