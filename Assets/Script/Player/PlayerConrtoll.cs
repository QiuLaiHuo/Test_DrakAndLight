using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Player
{
    public class PlayerConrtoll: MonoBehaviour
    {
        private PlayerInput inputs;
        private Rigidbody2D rd;
        private Animator anim;
    
        private PlayerAnimation planim;

    
        public GameObject WingAnimator;
        public GameObject WeaponAnimator;
        public Transform AttackPosition;

        [Header ("行动参数")]
        public float MoveSpeed;
        public float JumpForce;
        public float DoubleJumpForce;
        public float DodgeSpeed;
        public int facingDirection;

        [Header ("动作冷却")]
        public float DodgeCD;
        public float DodgeTime;
        public float AttackCD;
        private float CurrentAttack;
   

        [SerializeField]
        private Vector2 SpeedValue;
        private float CurrentDodgeTime;
        private float DodgeLife;
       

   
        [Header ("状态")]
        public bool IsDoubleJump = false;
        public bool IsGround = true;
        public bool IsJump = false;
        public bool IsAttack = false;
        public bool IsDefence = false;
        public bool IsPorfect = false;
        public bool IsDodge = false;
        public bool IsDodgeCD = false;


    



    

        private void Awake ()
        {
            inputs = new PlayerInput ();
            rd = GetComponent<Rigidbody2D> ();
            anim = GetComponent<Animator> ();
            planim = GetComponent<PlayerAnimation> ();

        }
        void Start ()
        {
            facingDirection = 1;
       
            inputs.GamePlay.Attack.started += Attack;
            inputs.GamePlay.Jump.started += Jump;
            inputs.GamePlay.PorfectDefence.started += PorfectBlock;
            inputs.GamePlay.Defence.performed += Defence;
            inputs.GamePlay.Defence.canceled += DefenceOver;
            inputs.GamePlay.Dodge.started += DodgeToReady;
        
            GameManager.Instance.InputEnable += InputEnable;
            GameManager.Instance.InputDisable += InputDisable;

        }



        private void DefenceOver (InputAction.CallbackContext context)
        {
            IsDefence = false;
            PlayerCharacter.Instance.state = State.Default;
        
            PlayerCharacter.Instance.shield.SetActive (false);

        }


        #region 帧调用
        void Update ()
        {
            SpeedValue = inputs.GamePlay.Move.ReadValue<Vector2> ();
            anim.SetFloat ("JumpVelocity",rd.velocity.y);
        
            DodgeTimer ();
       
            AttackTimer ();
            CheckShouldFlip ();

        }

        private void FixedUpdate ()
        {
        
            if (!IsAttack && !IsDefence && !IsPorfect && !IsDodge&&!PlayerCharacter.Instance.ISBack) { Move (); }

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
                    PlayerCharacter.Instance.DodgeInvincible (DodgeTime);
                    if (facingDirection == 1)
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
            if (IsGround && !IsAttack&& !PlayerCharacter.Instance.IsBreak)
            {
                PlayerCharacter.Instance.state = State.Defence;
                IsDefence = true;
                PlayerCharacter.Instance.shield.SetActive (true);
                rd.velocity = new Vector2 (0,rd.velocity.y);

            }
        }

        private void PorfectBlock (InputAction.CallbackContext context)
        {
            if (IsGround && !IsAttack&& !PlayerCharacter.Instance.IsBreak)
            {
           
                PlayerCharacter.Instance.state = State.Porfect;
                IsPorfect = true;
                PlayerCharacter.Instance.shield.SetActive (true);
                rd.velocity = new Vector2 (0,rd.velocity.y);
                StartCoroutine (PorfectDefence ());
            }
        }

        private IEnumerator PorfectDefence ()
        {
        
            yield return new WaitForSeconds (0.2f);
            IsPorfect = false;
       
            PlayerCharacter.Instance.state = State.Defence;
        }


        private void Jump (InputAction.CallbackContext obj)
        {


            if (IsGround)
            {
                IsJump = true;
                rd.AddForce (Vector2.up * JumpForce,ForceMode2D.Impulse);
            
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
           

        
        }



        private void Move ()
        {

            rd.velocity = new Vector2 (SpeedValue.x * MoveSpeed,rd.velocity.y);
            anim.SetFloat ("Move",Mathf.Abs (SpeedValue.x));


        }
        #endregion

        private void CheckShouldFlip ()
        {
            if (SpeedValue.x != 0f && SpeedValue.x != facingDirection
                                   && !PlayerCharacter.Instance.ISBack)
                Flip ();
        }

        private void Flip ()
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
            WeaponAnimator.GetComponent<Animator> ().SetTrigger ("Attack");
        }


        public void Death ()
        {

            InputDisable ();
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


        #region 输入系统的关闭和开启
        public void InputDisable()
        {
            inputs.GamePlay.Disable ();
       
        }

        public void InputEnable()
        {
            inputs.GamePlay.Enable ();
       
        }
        

        #endregion
    }
}
