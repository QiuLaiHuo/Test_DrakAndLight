using System.Collections;
using System.Collections.Generic;

using BehaviorDesigner.Runtime;

using UnityEngine;

public class BossStartTrigger : MonoBehaviour
{

    [SerializeField] GameObject Boss;
    private void OnTriggerEnter2D (Collider2D col)
    {
        
        if(col.CompareTag("Player")&&Boss!=null)
        {
            Boss.GetComponent<BehaviorTree> ().enabled = true;
            Destroy (gameObject,0.3f);
        }
    }

  
}
