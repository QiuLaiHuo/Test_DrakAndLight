using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.AI;
using BehaviorDesigner.Runtime.Tasks;


[TaskCategory("BrotherConditional")]
[TaskDescription("检测自身血量进行转阶段判定")]
public class PhaseTwo : EnemyConditionals
{
    private bool transiPhase;

    public override TaskStatus OnUpdate ()
    {
        transiPhase = character.CurrentHealth <= (characterData.Value.Health/ 2);
        return transiPhase == true ? TaskStatus.Success : TaskStatus.Failure;
    }
}
