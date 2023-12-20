using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.AI;
using DG.Tweening;
using BehaviorDesigner.Runtime.Tasks;
using UnityEditor.Experimental.GraphView;

public class JumpReady : EnemyAction
{
    public float HorizontalForce = 5.0f;
    public float JumpForce = 10f;
    public float ReadyTime;
    

    public string AnimName;
    private Tween tween;

    public override void  OnStart()
    {
        Debug.Log(anim.name);
        tween=  DOVirtual.DelayedCall (ReadyTime,JumpToReady,false);
        anim.SetTrigger (AnimName);
    }


    private void JumpToReady()
    {
        var dir = Target.Value.transform.position.x < transform.position.x ? -1 : 1;
        rd.AddForce (new Vector2 (dir * HorizontalForce,JumpForce),ForceMode2D.Impulse);

    }

    public override TaskStatus OnUpdate ()
    {
        if (rd.velocity.y < 0)
            return TaskStatus.Success;
        else return TaskStatus.Running;
    }

    public override void OnEnd ()
    {
        tween?.Kill ();
    }
}




