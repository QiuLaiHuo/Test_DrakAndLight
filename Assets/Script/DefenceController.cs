using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

public class DefenceController : MonoBehaviour
{
    public  float DefenceTimeCD;
    [SerializeField]
    private float CurrentDefenceTime;
    bool IsDefence;

    private void Start ()
    {
        CurrentDefenceTime = 0;
    }
    private void Update ()
    {
        if (IsDefence)
        {
            CurrentDefenceTime = DefenceTimeCD;
            CurrentDefenceTime-=Time.deltaTime;
            if (CurrentDefenceTime <= 0)
            {
                CurrentDefenceTime = 0;
                IsDefence = false;
            }

        }
    }
}
