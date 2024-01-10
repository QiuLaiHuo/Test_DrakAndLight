using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.AI;
using DG.Tweening;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("BrotherAction")]
public class Die : EnemyAction
{
    public string AnimaName;

    public float Over;
    private bool TimeOver=false;
    private Tween _tween;
    public override void OnStart ()
    {
        anim.SetTrigger (AnimaName);
        _tween = DOVirtual.DelayedCall (Over,() => { 
        
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

        _tween?.Kill ();
    }

}
