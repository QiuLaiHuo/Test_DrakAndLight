using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController: MonoBehaviour
{
    SpriteRenderer sprite;
    Rigidbody2D rd;

    private InputAsset inputAsset;
    private PlayerInput playerInput;
    private InputAction MoveAction;
    private Animator anim;

    [Header ("运动属性")]
    public float MoveSpeed;
    public float JumpForce;

    [Header ("格挡范围属性")]
    public Vector2 GizmosOffset;
    public Vector2 GizmsSize;
   
    public LayerMask layer;

    private void Awake ()
    {
        inputAsset = new InputAsset ();
        rd = GetComponent<Rigidbody2D> ();
        sprite = GetComponent<SpriteRenderer> ();
        playerInput = GetComponent<PlayerInput> ();
        anim = GetComponent<Animator> ();
        MoveAction = inputAsset.GamePlay.Move;
        // PlayerAttackInput = inputAsset.GamePlay.Attack;

    }
    void Start ()
    {

    }


    void Update ()
    {

    }

    private void FixedUpdate ()
    {
        Move ();
    }

    public void Move ()
    {
        if (MoveAction.ReadValue<Vector2> ().x <= -0.1f)
        {
            sprite.flipX = true;
        }
        else if (MoveAction.ReadValue<Vector2> ().x >= 0.1f)
        {
            sprite.flipX = false;
        }
        rd.velocity = new Vector2 (MoveAction.ReadValue<Vector2> ().x * MoveSpeed * Time.deltaTime,rd.velocity.y);
        //if (Mathf.Abs (rd.velocity.x) >= 0.1f)
        anim.SetFloat ("Run",Mathf.Abs (rd.velocity.x));
    }

    public void Jump (InputAction.CallbackContext context)
    {
        if (context.started)
        {
            rd.AddForce (Vector2.up * JumpForce,ForceMode2D.Impulse);
        }
    }

    public void Attack (InputAction.CallbackContext context)
    {

        if (context.started)
            anim.SetTrigger ("Attack");
    }


    public void Denfense (InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            anim.SetBool ("Defense",true);
           // RaycastHit2D[] hit2Ds = Physics2D.BoxCastAll (new Vector2 (transform.position.x + GizmosOffset.x,transform.position.y + GizmosOffset.y),GizmsSize);
        }
    }
    private void OnEnable ()
    {
        inputAsset.Enable ();
    }

    private void OnDisable ()
    {
        inputAsset.Disable ();
    }

    private void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        if(sprite.flipX)
        {
            Gizmos.DrawWireCube (new Vector3 (transform.position.x - GizmosOffset.x,transform.position.y + GizmosOffset.y,0),new Vector3 (GizmsSize.x,GizmsSize.y,0));
        }
        else if(!sprite.flipX)
        { Gizmos.DrawWireCube (new Vector3 (transform.position.x + GizmosOffset.x,transform.position.y + GizmosOffset.y,0),new Vector3 (GizmsSize.x,GizmsSize.y,0)); } 
    }
       

}
