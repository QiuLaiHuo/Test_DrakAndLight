using BehaviorDesigner.Runtime;

using Cinemachine;

using UnityEngine;

public class EnemyController: MonoBehaviour
{

    private BehaviorTree tree;
   
    private CinemachineImpulseSource cinema;
    private Animator anim;
    private Rigidbody2D rd;
    private int facingDirection = -1;
    private int curretfacing;
    
    [SerializeField] private float CheckRange;
    [SerializeField] private LayerMask CheckWhat;

    private Collider2D CheckCol;

    
    private void Awake ()
    {
        rd = GetComponent<Rigidbody2D> ();
        tree = GetComponent<BehaviorTree> ();
        anim = GetComponent<Animator> ();
        
        cinema = GetComponent<CinemachineImpulseSource> ();
    }

    private void FixedUpdate ()
    {
        CheckPlayer ();
    }

    //摄像机震动方法
    private void OnImpulseSource ()
    {
        cinema.GenerateImpulse (new Vector3 (Random.Range (-0.3f,0.3f),0f,0f));
    }


    public void PassivityToTreeEvent ()
    {
        tree?.SendEvent ("Onporfect");
       
        //anim.StopPlayback ();
       
    }

    public void PassivityAnim()
    { Debug.Log ("被弹刀");
         anim.SetTrigger ("Passivity");
        
    }
    

    public void Die ()
    {
        tree?.SendEvent ("OnDie");
    }


    private void CheckShouldFlip ()
    {
        if (CheckCol.transform.position.x > transform.position.x && facingDirection != 1)
        {
            //currentface *= -1;
            Flip ();
        }
        if (CheckCol.transform.position.x < transform.position.x && facingDirection != -1)
            Flip ();
    }

    private void Flip ()
    {
        facingDirection *= -1;
        //curretfacing = facingDirection;
        rd.transform.Rotate (0f,180f,0f);
    }


    private void CheckPlayer ()
    {
        CheckCol = Physics2D.OverlapCircle (transform.position,CheckRange,CheckWhat);
        if (CheckCol != null)
        {
            CheckShouldFlip ();
        }
    }

    private void OnDrawGizmos ()
    {
        Gizmos.DrawWireSphere (transform.position,CheckRange);
    }
    //private void Update () { }

    //public void Break()
    //{
    //    anim.SetBool ("Break",true);
    //}
}
