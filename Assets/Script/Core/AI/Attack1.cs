using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using Core.AI;

using DG.Tweening;

public class Attack1: EnemyAction
{
    public string AnimaName;
    public float AttackOverTime;
    public float Force;
    private bool Over = false;
    private Tween Tween;
    public override void OnStart ()
    {
        anim.SetTrigger (AnimaName);

        Tween = DOVirtual.DelayedCall (AttackOverTime,() =>
          {
              var dir = Target.Value.transform.position.x < transform.position.x ? -1 : 1;
              rd.AddForce (new Vector2 (dir* Force,0),ForceMode2D.Impulse);
              Over = true;
          }
              ,false);
    }


    public override TaskStatus OnUpdate ()
    {
        if (Over)
        {
           
            return TaskStatus.Success;
        }


        else return TaskStatus.Running;
    }

    public override void OnEnd ()
    {
        Over = false;
        Tween?.Kill ();
    }

}
