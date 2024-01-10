using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.AI;
using DG.Tweening;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("BrotherAction")]

public class RunReady : EnemyAction
{
    public string AnimaName;
    public float AttackOverTime;
    private bool Over = false;
    private Tween _tween;
    public override void OnStart ()
    {
        anim.SetTrigger (AnimaName);

        _tween = DOVirtual.DelayedCall (AttackOverTime,() =>
        {
            Over = true;
        }
              ,false);
    }


    public override TaskStatus OnUpdate ()
    {
        //sprit.flipX = Target.Value.transform.position.x > transform.position.x ? true : false;
        if (Over)
        {
            
            return TaskStatus.Success;
        }


        else return TaskStatus.Running;
    }

    public override void OnEnd ()
    {
        Over = false;
        _tween?.Kill ();
    }

}
