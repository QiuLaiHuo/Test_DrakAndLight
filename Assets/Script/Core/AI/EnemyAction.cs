using System.Collections;
using System.Collections.Generic;

using BehaviorDesigner.Runtime.Tasks;

using BehaviorDesigner.Runtime;
using UnityEngine;
using Cinemachine;

namespace Core.AI
{
    [TaskCategory("BrotherAction/Basic")]
    public class EnemyAction: Action
    {
        protected Rigidbody2D rd;
        protected EnemyController enemy;
        protected Animator anim;
        public SharedGameObject Target;

        public SharedCharacterData CharacterData;
        //protected SpriteRenderer sprit;
        //protected  AttackControll attackControll;


        protected CinemachineImpulseSource camera;
        public override void OnAwake ()
        {
            
            rd = gameObject.GetComponent<Rigidbody2D> ();
            //attack = gameObject.GetComponent<AttackControll> ();
            anim = gameObject.GetComponent<Animator> ();
            //sprit = gameObject.GetComponent<SpriteRenderer> ();
            camera = gameObject.GetComponent <CinemachineImpulseSource> (); 
            enemy = gameObject.GetComponent<EnemyController> ();
        }

       


    }
}