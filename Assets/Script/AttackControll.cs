using UnityEngine;

public class AttackControll: MonoBehaviour
{
    [Header ("��������")]
    public int Damage;


    public Controller controller;






    private void OnTriggerEnter2D (Collider2D other)
    {
       
            Debug.Log (other.name + "�����ߣ�" + gameObject.name);

            if (other.CompareTag ("Player") || other.CompareTag ("Enemy"))
                other.GetComponent<Character> ()?.OnTakeDamage (this);
           

    }

    public void PassivityDamage ()
    {
        Debug.Log ("������");
        controller.anim.StopPlayback ();
        controller.anim.SetTrigger ("Passivity");
    }
}
