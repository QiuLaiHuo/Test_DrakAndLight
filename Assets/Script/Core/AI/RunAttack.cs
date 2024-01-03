using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.AI;
using DG.Tweening;
using BehaviorDesigner.Runtime.Tasks;



[TaskCategory("BrotherAction")]
public class RunAttack : EnemyAction
{
    public string AnimaName;
    public float AnimaOverTime;
    public float Force;
    private bool Over = false;
    private Tween Tween;

    public override void OnStart ()
    {
        anim.SetTrigger (AnimaName);
        Tween = DOVirtual.DelayedCall (AnimaOverTime,() => {
            var dir = Target.Value.transform.position.x < transform.position.x ? -1 : 1;
            rd.AddForce (new Vector2 (dir * Force,0),ForceMode2D.Impulse);
            Over = true;
        
        },false);
    }


    public override TaskStatus OnUpdate ()
    {
        if (Over)
        {         
            return TaskStatus.Success;
        }
        else
            return TaskStatus.Running;
           
    }

    public override void OnEnd ()
    { Over = false;
        Tween?.Kill ();
    }




}

