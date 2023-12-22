using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.AI;
using BehaviorDesigner.Runtime.Tasks;
using Unity.Mathematics;

public class AttackScopeConditionals : EnemyConditionals
{
    public float Scope = 0f;

    public override void OnStart ()
    {

       
    }
    public override TaskStatus OnUpdate ()
    {
     // Debug.Log(math.abs (Target.Value.transform.position.x-transform.position.x));
            if(math.abs(Target.Value.transform.position.x - transform.position.x) <=Scope)
                return TaskStatus.Success; else return TaskStatus.Failure;
    }
}
