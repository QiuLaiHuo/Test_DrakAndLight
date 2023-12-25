using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.AI;
using DG.Tweening;
using BehaviorDesigner.Runtime.Tasks;

public class HitBack : EnemyAction
{
    private bool Over =false;
    public float OverTime;
    private Tween tween;

    public override void OnStart()
    {
        attackControll.PassivityDamage ();
        tween = DOVirtual.DelayedCall (OverTime,() => { 
        
        Over = true;
        
        },false);
    }


    public override TaskStatus OnUpdate ()
    {
        if (Over)
            return TaskStatus.Success;
        else return TaskStatus.Running;
    }

    public override void OnEnd ()
    {
        Over = false;
        tween?.Kill ();
    }
}
