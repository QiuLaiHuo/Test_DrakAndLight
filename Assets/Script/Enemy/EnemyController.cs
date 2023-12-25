using System.Collections;
using System.Collections.Generic;

using Cinemachine;

using UnityEngine;

public class EnemyController : Controller
{
    // Start is called before the first frame update
    //private Animator anim;
    private SpriteRenderer sprite;
    private CinemachineImpulseSource cinema;

    private void Awake ()
    {
        anim = GetComponent<Animator> ();
        sprite = GetComponent<SpriteRenderer> ();
        cinema = GetComponent<CinemachineImpulseSource> ();
    }

    private void OnImpulseSource()
    {
        cinema.GenerateImpulse (new Vector3(Random.Range(-0.3f,0.3f),0f,0f));
    }
   

    //private void Update () { }

    //public void Break()
    //{
    //    anim.SetBool ("Break",true);
    //}
}
