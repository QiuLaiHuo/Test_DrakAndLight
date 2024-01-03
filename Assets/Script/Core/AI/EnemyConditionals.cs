using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


namespace Core.AI
{
    
    [TaskCategory("BrotherConditional/Basic")]
    public class EnemyConditionals: Conditional
    {
        protected Rigidbody2D rd;
       // protected AttackControll attack;
        protected Animator anim;
        public SharedGameObject Target;
        protected EnemyCharacter character;

        public SharedCharacterData characterData;

        public override void OnAwake ()
        {
           
        
            rd = gameObject.GetComponent<Rigidbody2D> ();
           // attack = gameObject.GetComponent<AttackControll> ();
            anim = gameObject.GetComponent<Animator> ();
            character = gameObject.GetComponent<EnemyCharacter> ();
        }

    }
}