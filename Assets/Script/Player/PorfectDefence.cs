using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorfectDefence : MonoBehaviour
{
    public Character character;
    //private Animator anim;
    private void OnEnable ()
    {
       // anim = GetComponent<Animator> ();
        character.state = State.Porfect;
        
    }
    private void FixedUpdate ()
    {
        character.state = State.Porfect;
    }

    private void OnDisable ()
    {
        character.state = State.Default;
    }
}
