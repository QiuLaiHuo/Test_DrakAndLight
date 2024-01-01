using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private PlayerConrtoll player;
    private PlayerCharacter chara;
    //private DefenceController defence;
    [SerializeField]private GameObject Dodge;

    private void Awake ()
    {
        anim = GetComponent<Animator>(); 
        player = GetComponent<PlayerConrtoll>();
        chara= GetComponent<PlayerCharacter>();
        //defence = GetComponent<DefenceController>();
    }

    void Update()
    {
        SetAnimation ();
    }

    void SetAnimation()
    {
        anim.SetBool ("Jump",player.IsJump);
        anim.SetBool ("IsGround",player.IsGround);
        anim.SetBool ("DoubleJump",player.IsDoubleJump);
        //anim.SetBool ("Attack",player.IsAttack);
        anim.SetBool ("Defence",player.IsDefence);
        anim.SetBool ("Death",chara.IsDeath);
        anim.SetBool ("PorfectDefence",player.IsPorfect);
        anim.SetBool ("Dodge",player.IsDodge);
    }

    public void Hurt()
    {
        anim.SetTrigger ("Hurt");
    }

    public void Attack()
    {
        anim.SetTrigger ("Attack");
    }
    public void DodgeOK()
    {
        Dodge.GetComponent<Animator> ()?.SetTrigger ("DodgeOK");
    }
}
