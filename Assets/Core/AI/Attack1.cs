using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.AI;
using DG.Tweening;
using BehaviorDesigner.Runtime.Tasks;

public class Attack1 : EnemyAction
{
    public string AnimaName;
    public float AttackOverTime;
    private bool Over = false;
    public override void OnStart ()
    {
        anim.SetTrigger (AnimaName);

        DOVirtual.DelayedCall (AttackOverTime,() =>
        {
            Over = true;
        }
            ,false);
    }


    public override TaskStatus OnUpdate ()
    {
        if (Over)
            return TaskStatus.Success;
        else return TaskStatus.Running;
    }

}
