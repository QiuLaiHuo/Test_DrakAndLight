using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
    [SerializeField]private Animator anim;

    public static Func<float> ShieldChange;


    public static void Register()
    {

    }

    public static void OutRegister()
    {

    }

    private void Update ()
    {
        anim.SetFloat ("Blend",ShieldChange.Invoke ());
    }
}
