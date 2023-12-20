using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.AI;
using BehaviorDesigner.Runtime.Tasks;

public class FacePlayer :EnemyAction
{
    public override TaskStatus OnUpdate ()
    {
        sprit.flipX = Target.Value.transform.position.x<transform.position.x?false : true;  
        return TaskStatus.Success;
    }
}
