using System.Collections;
using System.Collections.Generic;

using BehaviorDesigner.Runtime.Tasks;

using UnityEngine;



namespace Core.AI
{
    public class EnemyAction: Action
    {
        protected Rigidbody2D rd;
        protected AttackControll attack;
        protected Animator anim;


        public void Awake ()
        {
            rd = GetComponent<Rigidbody2D> ();
            attack = GetComponent<AttackControll> ();
            anim = GetComponent<Animator> ();
        }



    }
}