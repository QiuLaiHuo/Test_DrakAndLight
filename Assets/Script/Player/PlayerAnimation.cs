using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private PlayerConrtoll player;
    private Character chara;
    private DefenceController defence;

    private void Awake ()
    {
        anim = GetComponent<Animator>(); 
        player = GetComponent<PlayerConrtoll>();
        chara= GetComponent<Character>();
        defence = GetComponent<DefenceController>();
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
        anim.SetBool ("Attack",player.IsAttack);
        anim.SetBool ("Defence",player.IsDefence);
        anim.SetBool ("Death",chara.IsDeath);
    }

    public void Hurt()
    {
        anim.SetTrigger ("Hurt");
    }

    
}
