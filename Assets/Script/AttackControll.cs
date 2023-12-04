using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackControll : MonoBehaviour
{
    [Header ("必要组件")]
    private SpriteRenderer sprite;
    private Animator anim;
    private BoxCollider2D coll;

    private void Awake ()
    {
        anim = GetComponent<Animator> ();
        sprite = GetComponent<SpriteRenderer> ();
        coll = GetComponent<BoxCollider2D> ();
    }
    void Start()
    {
        sprite.enabled = false;
        coll.enabled = false;
    }

   
}
