using UnityEngine;

public class AttackControll: MonoBehaviour
{
    [Header ("¹¥»÷ÊôÐÔ")]
    public int Damage;

    private void OnTriggerStay2D (Collider2D other)
    {
        //Debug.Log (other.name);
        if (other.CompareTag ("Defence"))
        {
            other.GetComponent<DefenceController> ()?.OnDefence (this);
            return;
        }
        else if (other.CompareTag ("Player") || other.CompareTag ("Enemy"))
        { other.GetComponent<Character> ()?.OnTakeDamage (this); return; }
    }




}
