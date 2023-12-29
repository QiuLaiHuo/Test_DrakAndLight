using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

using UnityEngine;

[CreateAssetMenu(fileName ="New Character Data",menuName = "Data/Character Data")]
public class CharacterData : ScriptableObject
{

    [Header("��������")]
    public int Health=50;
    public int Shield=20;

    [Header("��������")]
    public float RecoverTime=5f;
    public float InvincibleTime=0.4f;

   

}

