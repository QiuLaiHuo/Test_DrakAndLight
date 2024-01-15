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
   

    private void Start()
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
