using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
     [SerializeField]private Animator Shieldanim;
     [SerializeField] private Animator Lifeanima;

    public static Func<float> OnShieldChange;
    public static Func<float> OnLifeChange;
   
private void OnDestroy ()
    {
        Delegate[] s = OnShieldChange.GetInvocationList ();
        foreach (var item in s)
        {
            OnShieldChange -= item as Func<float>;
        }
        Delegate[] a = OnLifeChange.GetInvocationList ();
        foreach (var item in a)
        {
            OnLifeChange -= item as Func<float>;
        }
        //PlayerCharacter.Instance.OnShieldChange -= PlayerShieldUI;
        //PlayerCharacter.Instance.OnLifeChange -= PlayerLifeUI;
    }
    private void Awake()
    {
        PlayerCharacter.Instance.OnShieldChange += PlayerShieldUI;
        PlayerCharacter.Instance.OnLifeChange += PlayerLifeUI;
    }

    private void PlayerShieldUI()
    {
        Shieldanim.SetFloat ("Blend",OnShieldChange.Invoke ());
    }

    private void PlayerLifeUI()
    {
        Lifeanima.SetFloat("Blend",OnLifeChange.Invoke());
    }

    
}
