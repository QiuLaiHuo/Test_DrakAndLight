using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConrtoll : MonoBehaviour
{
    [Header("必要组件")]
    public  PlayerInput inputs;
    public Rigidbody2D rd;
    public SpriteRenderer sprit;


    [Header ("行动参数")]
    public float MoveSpeed;
    public float JumpForce;
    public float DoubleJumpForce;
    [SerializeField]
    private Vector2 SpeedValue;
    private bool DoubleJump = false;

    private void Awake ()
    {
        inputs = new PlayerInput ();
        rd = GetComponent<Rigidbody2D> ();
        sprit = GetComponent<SpriteRenderer> ();

    }
    void Start()
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
       SpeedValue =  inputs.GamePlay.Move.ReadValue<Vector2> ();
    }

    private void FixedUpdate ()
    {
        Move ();
    }
    #endregion


    #region 角色操作方法
    private void Defence (InputAction.CallbackContext obj)
    {
        
    }

    private void Jump (InputAction.CallbackContext obj)
    {
        rd.AddForce (Vector2.up*JumpForce,ForceMode2D.Impulse);
    }

    private void Attack (InputAction.CallbackContext obj)
    {
        
    }

    private void Move ()
    {
        if (SpeedValue.x < -0.1f)
        {
            sprit.flipX = false;
           
        }
        else if(SpeedValue.x>0.1f){ sprit.flipX = true;  }

        rd.velocity = new Vector2 (SpeedValue.x * MoveSpeed,rd.velocity.y);
        Debug.Log (SpeedValue.x);
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
}
