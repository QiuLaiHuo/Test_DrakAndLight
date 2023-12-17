using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Controller
{
    // Start is called before the first frame update
    //private Animator anim;


    private void Awake ()
    {
        anim = GetComponent<Animator> ();
    }
   

    //public void Break()
    //{
    //    anim.SetBool ("Break",true);
    //}
}
