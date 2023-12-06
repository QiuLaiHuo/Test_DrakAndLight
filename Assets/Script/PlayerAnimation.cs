using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private PlayerConrtoll player;


    private void Awake ()
    {
        anim = GetComponent<Animator>(); 
        player = GetComponent<PlayerConrtoll>();
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
    }
}
