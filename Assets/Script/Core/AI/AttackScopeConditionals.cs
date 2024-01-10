using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.AI;
using BehaviorDesigner.Runtime.Tasks;
using Unity.Mathematics;


[TaskCategory("BrotherConditional")]
[TaskDescription("检测目标距离")]
public class AttackScopeConditionals : EnemyConditionals
{
    public float MaxScope = 0f;
    public float MinScope = 0f;
    
    public override TaskStatus OnUpdate ()
    {
        float abs = math.abs(Target.Value.transform.position.x - transform.position.x);
    
        if(abs <=MaxScope&&abs>=MinScope)
            return TaskStatus.Success; else return TaskStatus.Failure;
    }
}
