using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.AI;
using BehaviorDesigner.Runtime.Tasks;
using Unity.Mathematics;
using UnityEngine.UI;

public class RunLoop : EnemyAction
{
    public string AnimaName;
    public float RunSpeed;
    public float Scope;

    public override void OnStart ()
    {
        anim.SetTrigger (AnimaName);
    }
    public override TaskStatus OnUpdate ()
    {
        sprit.flipX = Target.Value.transform.position.x > transform.position.x ? true : false;
        var dir = Target.Value.transform.position.x < transform.position.x ? -1 : 1;

        if (math.abs (Target.Value.transform.position.x - transform.position.x) > Scope)
        {
            rd.velocity = new Vector2 (dir * RunSpeed,rd.velocity.y);
            
        }
        else if (math.abs (Target.Value.transform.position.x - transform.position.x) <= Scope)
        {
            rd.velocity = new Vector2 (0,rd.velocity.y);

            return TaskStatus.Success;
        }
            
        return TaskStatus.Running;
    }

}
