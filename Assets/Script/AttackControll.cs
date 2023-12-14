using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackControll : MonoBehaviour
{
    [Header ("¹¥»÷ÊôÐÔ")]
    public int Damage;

    private void OnTriggerStay2D (Collider2D other)
    {
        Debug.Log (other.name);
        other.GetComponent<Character> ()?.OnTakeDamage (this);
    }



}
