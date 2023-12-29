using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

using UnityEngine;


[CreateAssetMenu(fileName ="New Attack Data",menuName = "Data/Attack Data")]
public class AttackData : ScriptableObject
{
    [Header ("¹¥»÷²ÎÊı")]
   public float distance = 0.5f;
   public LayerMask WhatIsVictim;


    [Header ("ÉËº¦ÊôĞÔ")]
    public int Damage = 1;
    public int ShieldDamage = 2;
    public int DamageMultiply = 2;

}

