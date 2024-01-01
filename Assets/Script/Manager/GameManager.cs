using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

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


    //private void OnEnable ()
    //{
    //    DontDestroyOnLoad (gameObject);
    //}

    //[SerializeField] private PlayerConrtoll playerConrtoll;

    

}
