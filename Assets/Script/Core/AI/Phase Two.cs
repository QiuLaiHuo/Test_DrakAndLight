using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.AI;
using BehaviorDesigner.Runtime.Tasks;

public class PhaseTwo : EnemyConditionals
{
    private bool transiPhase;

    public override TaskStatus OnUpdate ()
    {
        transiPhase = character.CurrentHealth <= (characterData.Value.Health/ 2);
        return transiPhase == true ? TaskStatus.Success : TaskStatus.Failure;
    }
}
