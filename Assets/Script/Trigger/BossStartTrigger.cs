
using System.Collections;
using BehaviorDesigner.Runtime;

using UnityEngine;

public class BossStartTrigger : MonoBehaviour
{

    [SerializeField] BehaviorTree Boss;
    [SerializeField]private Collider2D coll;
    private void OnTriggerEnter2D (Collider2D col)
    {
        
        if(col.CompareTag("Player")&&Boss!=null)
        {

            StartCoroutine(BossStart());
           
        }
    }

    IEnumerator BossStart()
    {
         Boss.EnableBehavior();
         Destroy(coll);
        AudioManager.Instance.AudioPlay(audioType:AudioType.BGM1);
         
         yield return null;
          Destroy (gameObject,0.3f);
    }

   
  
}
