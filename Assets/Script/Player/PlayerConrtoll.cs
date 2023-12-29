using System.Collections;

using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerConrtoll: Controller
{
    [Header ("必要组件")]
    public PlayerInput inputs;
    public Rigidbody2D rd;
    public SpriteRenderer sprit;
    //public Animator anim;
    public Character chara;
    public GameObject WingAnimator;
    public GameObject WeaponAnimator;
    private PlayerAnimation planim;
   public AttackData AttackData;
    public CharacterData CharacterData;
    public AttackDetailed AttackDetailed;
    public Transform AttackPosition;

    [Header ("行动参数")]
    public float MoveSpeed;
    public float JumpForce;
    public float DoubleJumpForce;
    public float DodgeSpeed;
    public State state;
    public int facingDirection;

    [Header ("动作冷却")]
    public float DodgeCD;
    public float DodgeTime;
    public float AttackCD;
    private float CurrentAttack;
    //public float AttackTimeCD;

    [SerializeField]
    private Vector2 SpeedValue;
    private float CurrentDodgeTime;
    private float DodgeLife;

    //private float CurrentDefenceTime;
    //    private float CurrentAttackTime;
    [Header ("状态")]
    public bool IsDoubleJump = false;
    public bool IsGround = true;
    public bool IsJump = false;
    public bool IsAttack = false;
    public bool IsDefence = false;
    public bool IsPorfect = false;
    public bool IsDodge = false;
    public bool IsDodgeCD = false;

    // public bool DefenceCD = false;
    // public bool AttackCD = false;



    //private EdgeCollider2D GroundCheck;

    private void Awake ()
    {
        inputs = new PlayerInput ();
        rd = GetComponent<Rigidbody2D> ();
        sprit = GetComponent<SpriteRenderer> ();
        anim = GetComponent<Animator> ();
        chara = GetComponent<Character> ();
        planim = GetComponent<PlayerAnimation> ();

        // GroundCheck = GetComponent<EdgeCollider2D> ();
    }
    void Start ()
    {
        state = chara.state;
        //inputs.GamePlay.Move.started += Move;
        inputs.GamePlay.Attack.started += Attack;
        inputs.GamePlay.Jump.started += Jump;
        inputs.GamePlay.PorfectDefence.started += PorfectBlock;
        inputs.GamePlay.Defence.performed += Defence;
        inputs.GamePlay.Defence.canceled += DefenceOver;
        inputs.GamePlay.Dodge.started += DodgeToReady;
        //sprit.flipX = true;
        facingDirection = 1;
    }



    private void DefenceOver (InputAction.CallbackContext context)
    {
        IsDefence = false;
        state = State.Default;

    }


    #region 帧调用
    void Update ()
    {
        SpeedValue = inputs.GamePlay.Move.ReadValue<Vector2> ();
        anim.SetFloat ("JumpVelocity",rd.velocity.y);
        // Debug.Log (state);
        DodgeTimer ();
        //AttackTimer ();
        AttackTimer ();
        CheckShouldFlip ();
    }

    private void FixedUpdate ()
    {
        chara.state = state;
        if (!IsAttack && !IsDefence && !IsPorfect && !IsDodge) { Move (); }
        Dodge ();

    }
    #endregion


    #region 角色操作方法

    private void DodgeToReady (InputAction.CallbackContext context)
    {
        if (IsDodgeCD)
            return;

        IsDodge = true;
        IsDodgeCD = true;
        CurrentDodgeTime = DodgeCD;
        DodgeLife = DodgeTime;
    }

    private void Dodge ()
    {
        if (IsDodge)
        {
            if (DodgeLife > 0)
            {
                chara.DodgeInvincible (DodgeTime);
                if (facingDirection==1)
                {
                    rd.AddForce (Vector2.right * DodgeSpeed,ForceMode2D.Impulse);
                }
                else
                    rd.AddForce (Vector2.left * DodgeSpeed,ForceMode2D.Impulse);

                DodgeLife -= Time.deltaTime;
            }
            if (DodgeLife <= 0)
                IsDodge = false;
        }
    }
    private void Defence (InputAction.CallbackContext obj)
    {
        if (IsGround && !IsAttack)
        {
            state = State.Defence;
            IsDefence = true;
            //DefenceCD = true;
            // CurrentDefenceTime = DefenceTimeCD;
            rd.velocity = new Vector2 (0,rd.velocity.y);

        }
    }

    private void PorfectBlock (InputAction.CallbackContext context)
    {
        if (IsGround && !IsAttack)
        {
            //CurrentDefenceTime = DefenceCD;
            //PorfectCD = true;
            // IsDefence = false;
            state = State.Porfect;
            IsPorfect = true;
            rd.velocity = new Vector2 (0,rd.velocity.y);
            StartCoroutine (PorfectDefence ());
        }
    }

    private IEnumerator PorfectDefence ()
    {
        //yield return new WaitForFixedUpdate ();
        yield return new WaitForSeconds (0.2f);
        IsPorfect = false;
        // IsDefence = true;
        state = State.Default;
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
            IsJump = true;
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
        if (!IsDefence && !IsAttack)
        {
            CurrentAttack = AttackCD;
            rd.velocity = new Vector2 (0,rd.velocity.y);
            IsAttack = true;
            planim.Attack ();
        }
        else
            return;

        // AttackCD = true;
        // CurrentAttackTime = AttackTimeCD;
    }



    private void Move ()
    {

        //if (SpeedValue.x < -0.1f)
        //{
        //   // sprit.flipX = false;
        //    //weaponPosition.ToLeft ();

        //}
        //else if (SpeedValue.x > 0.1f) { //sprit.flipX = true;
        //                                weaponPosition.ToRight (); }

        rd.velocity = new Vector2 (SpeedValue.x * MoveSpeed,rd.velocity.y);
        anim.SetFloat ("Move",Mathf.Abs (SpeedValue.x));

        //if (IsGround && SpeedValue == Vector2.zero)
        //    anim.SetTrigger ("Idle");
    }
    #endregion

    private void CheckShouldFlip()
    {
       // Debug.Log (facingDirection);
        if (SpeedValue.x != 0f&&SpeedValue.x!=facingDirection)
            Flip ();
        

    }

    private void Flip()
    {
        facingDirection *= -1;
        rd.transform.Rotate (0f,180f,0f);
    }

    #region 碰撞状态检测方法
    private void OnCollisionEnter2D (Collision2D col)
    {
        if (col.collider.CompareTag ("Ground"))
        {
            IsGround = true;
            IsJump = false;
            IsDoubleJump = false;
            anim.SetBool ("IsGround",IsGround);
        }
    }
    private void OnCollisionExit2D (Collision2D col)
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

    #region 辅助函数
    public void Weapon ()
    {
       // WeaponAnimator.GetComponent<SpriteRenderer> ().flipX = sprit.flipX;
        WeaponAnimator.GetComponent<Animator> ().SetTrigger ("Attack");
    }

    public void Death ()
    {
        inputs.GamePlay.Disable ();

    }


    private void DodgeTimer ()
    {
        if (IsDodgeCD)
        {
            CurrentDodgeTime -= Time.deltaTime;
            if (CurrentDodgeTime <= 0)
            {
                CurrentDodgeTime = 0;
                IsDodgeCD = false;
                planim.DodgeOK ();
            }
        }
    }

    private void AttackTimer ()
    {
        if (IsAttack)
        {
            CurrentAttack -= Time.deltaTime;
            if (CurrentAttack <= 0)
            {
                CurrentAttack = 0;
                IsAttack = false;
            }
        }

    }
    #endregion
}
