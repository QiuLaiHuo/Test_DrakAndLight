

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] private Volume globalVolume;
    //[SerializeField]private float s;
    private Vignette dof;
   
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
        DOVirtual.DelayedCall(0.2f, () =>
        {
           dof?.smoothness.SetValue(new FloatParameter(0.1f));
            
        }, false);
    }
    private void Awake ()
    {
        DontDestroyOnLoad (gameObject);
    }

    //[SerializeField] private PlayerConrtoll playerConrtoll;

    

}
