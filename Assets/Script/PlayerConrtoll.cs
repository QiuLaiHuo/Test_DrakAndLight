using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerConrtoll: MonoBehaviour
{
    [Header ("必要组件")]
    public PlayerInput inputs;
    public Rigidbody2D rd;
    public SpriteRenderer sprit;
    public Animator anim;
    public GameObject WingAnimator;
    public GameObject WeaponAnimator;


    [Header ("行动参数")]
    public float MoveSpeed;
    public float JumpForce;
    public float DoubleJumpForce;
    [SerializeField]
    private Vector2 SpeedValue;
    public bool IsDoubleJump = false;
    public bool IsGround = true;
    public bool IsJump = false;
    public bool IsAttack = false;


    //private EdgeCollider2D GroundCheck;

    private void Awake ()
    {
        inputs = new PlayerInput ();
        rd = GetComponent<Rigidbody2D> ();
        sprit = GetComponent<SpriteRenderer> ();
        anim = GetComponent<Animator> ();

        // GroundCheck = GetComponent<EdgeCollider2D> ();
    }
    void Start ()
    {
        //inputs.GamePlay.Move.started += Move;
        inputs.GamePlay.Attack.started += Attack;
        inputs.GamePlay.Jump.started += Jump;
        inputs.GamePlay.Defence.started += Defence;
        sprit.flipX = true;
    }
    #region 帧调用
    void Update ()
    {
        SpeedValue = inputs.GamePlay.Move.ReadValue<Vector2> ();

        anim.SetFloat ("JumpVelocity",rd.velocity.y);

    }

    private void FixedUpdate ()
    {
        if(!IsAttack) { Move (); }
       

    }
    #endregion


    #region 角色操作方法
    private void Defence (InputAction.CallbackContext obj)
    {

    }

    private void Jump (InputAction.CallbackContext obj)
    {

        //TOOD：动画设置
        if (IsGround)
        {
            IsJump = true;
            rd.AddForce (Vector2.up * JumpForce,ForceMode2D.Impulse);
           // IsDoubleJump = false;
        }
        else if (!IsGround && !IsDoubleJump)
        {
            IsDoubleJump = true;
            rd.velocity = new Vector2 (rd.velocity.x,0);
            anim.SetTrigger ("DoubleJump");
            WingAnimator.GetComponent<Animator> ().SetTrigger ("Wing");

            rd.AddForce (Vector2.up * DoubleJumpForce,ForceMode2D.Impulse);

        }
        else if (IsDoubleJump)
            return;


    }

    private void Attack (InputAction.CallbackContext obj)
    {
        if (IsGround)
        { rd.velocity = new Vector2(0,rd.velocity.y);
        IsAttack  = true;

        }else if(!IsGround)
        {
            IsAttack = true;
        }
       
    }

    private void Move ()
    {

        if (SpeedValue.x < -0.1f)
        {
            sprit.flipX = false;


        }
        else if (SpeedValue.x > 0.1f) { sprit.flipX = true; }

        rd.velocity = new Vector2 (SpeedValue.x * MoveSpeed,rd.velocity.y);
        anim.SetFloat ("Move",Mathf.Abs (SpeedValue.x));

        if (IsGround && SpeedValue == Vector2.zero)
            anim.SetTrigger ("Idle");
    }
    #endregion




    #region 碰撞检测方法
    private void OnCollisionEnter2D (Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
        {
            IsGround = true;
            IsJump = false;
            IsDoubleJump = false;
            anim.SetBool ("IsGround",IsGround);
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag ("Ground"))
        {
            IsGround = false;
            anim.SetBool ("IsGround",IsGround);
        }
    }
    #endregion

    #region 启用和禁用方法调用
    private void OnEnable ()
    {
        inputs.GamePlay.Enable ();
    }

    private void OnDisable ()
    {
        inputs.GamePlay.Disable ();
    }

    #endregion


    public void Weapon ()
    {
        WeaponAnimator.GetComponent<SpriteRenderer> ().flipX = sprit.flipX;
        if (sprit.flipX == true)
            WeaponAnimator.GetComponent<Animator> ().SetTrigger ("Attack_R");
        else
            WeaponAnimator.GetComponent<Animator> ().SetTrigger ("Attack_L");
    }
}
