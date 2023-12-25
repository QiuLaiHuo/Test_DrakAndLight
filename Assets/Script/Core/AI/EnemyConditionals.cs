using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


namespace Core.AI
{
    public class EnemyConditionals: Conditional
    {
        protected Rigidbody2D rd;
        protected AttackControll attack;
        protected Animator anim;
        public SharedGameObject Target;
        protected Character character;


        public void Awake()
        {
            rd = GetComponent<Rigidbody2D> ();
            attack = GetComponent<AttackControll> ();
            anim = GetComponent<Animator> ();
            character = GetComponent<Character> ();
        }

    }
}