using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine;


namespace Core.AI
{
    public class ShootAttack : Attack1
    {
        public override void OnStart()
        {
            base.OnStart();
           
        }

        public override TaskStatus OnUpdate()
        {
            if (Over)
            {
                enemy.Shoot();
                return TaskStatus.Success;
            }


            else return TaskStatus.Running;
        }

        public override void OnEnd()
        {
            base.OnEnd();
        }
    }
}

