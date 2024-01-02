using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName ="New Attack Data",menuName = "Data/Attack Data")]
public class AttackData : ScriptableObject
{
  
    [Header ("伤害属性")]
    public int Damage = 1;
    public int ShieldDamage = 2;
    public int DamageMultiply = 2;
    public Vector2 beatForce = new Vector2(2f,2f);

}

