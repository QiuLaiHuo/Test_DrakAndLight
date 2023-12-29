using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

using UnityEngine;

[CreateAssetMenu(fileName ="New Character Data",menuName = "Data/Character Data")]
public class CharacterData : ScriptableObject
{

    [Header("基本属性")]
    public int Health=50;
    public int Shield=20;

    [Header("抗击属性")]
    public float RecoverTime=5f;
    public float InvincibleTime=0.4f;

   

}

