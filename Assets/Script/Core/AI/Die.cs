using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.AI;
using DG.Tweening;
using BehaviorDesigner.Runtime.Tasks;

public class Die : EnemyAction
{
    public string AnimaName;

    public float Over;
    private bool TimeOver=false;
    private Tween tween;

    public override void OnStart ()
    {
        anim.SetTrigger (AnimaName);
        tween = DOVirtual.DelayedCall (Over,() => { 
        
            TimeOver = true;    
        
        },false);
    }


    public override TaskStatus OnUpdate ()
    {
        if(TimeOver)
            return TaskStatus.Success;
        else return TaskStatus.Running;
    }

    public override void OnEnd ()
    {
        TimeOver=false;

        tween?.Kill ();
    }

}
