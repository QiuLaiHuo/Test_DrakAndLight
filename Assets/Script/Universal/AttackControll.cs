using BehaviorDesigner.Runtime;

using UnityEngine;

public class AttackControll: MonoBehaviour
{
    [Header ("¹¥»÷ÊôÐÔ")]
    public int Damage;


    public Controller controller;

    private BehaviorTree tree;

    private void Awake ()
    {
        tree = GetComponent<BehaviorTree> ();
    }


    private void OnTriggerEnter2D (Collider2D other)
    {
       
            Debug.Log (other.name + "¹¥»÷Õß£º" + gameObject.name);

            if (other.CompareTag ("Player") || other.CompareTag ("Enemy"))
                other.GetComponent<Character> ()?.OnTakeDamage (this);
           

    }

    public void PassivityDamage ()
    {
        tree?.SendEvent ("Onporfect");
        Debug.Log ("±»µ¯µ¶");
        controller.anim.StopPlayback ();
        controller.anim.SetTrigger ("Passivity");
    }

    public void Die ()
    {
        tree?.SendEvent ("OnDie");
    }
}
