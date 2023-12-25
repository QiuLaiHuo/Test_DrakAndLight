using System.Collections;
using System.Collections.Generic;

using BehaviorDesigner.Runtime.Tasks;

using BehaviorDesigner.Runtime;
using UnityEngine;
using Cinemachine;

namespace Core.AI
{
    public class EnemyAction: Action
    {
        protected Rigidbody2D rd;
        protected AttackControll attack;
        protected Animator anim;
        public SharedGameObject Target;
        protected SpriteRenderer sprit;
        protected  AttackControll attackControll;


        protected CinemachineImpulseSource camera;
        public override void OnAwake ()
        {
            
            rd = gameObject.GetComponent<Rigidbody2D> ();
            attack = gameObject.GetComponent<AttackControll> ();
            anim = gameObject.GetComponent<Animator> ();
            sprit = gameObject.GetComponent<SpriteRenderer> ();
            camera = gameObject.GetComponent <CinemachineImpulseSource> (); 
            attackControll = gameObject.GetComponent<AttackControll> ();
        }

       


    }
}