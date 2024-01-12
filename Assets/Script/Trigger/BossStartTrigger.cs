
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
            AudioManager.Instance.AudioPlay(audioType:AudioType.BGM1);
            Destroy (gameObject,0.3f);
        }
    }

  
}
