using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using Core.AI;

using DG.Tweening;


[TaskCategory("BrotherAction")]
public class Attack1: EnemyAction
{
    public string AnimaName;
    public float AttackOverTime;
    public float AudioTime;
    public float Force;
    protected bool Over = false;
    private Tween _tween; 
    private Tween _tween1;

    public override void OnStart ()
    {
        anim.SetTrigger (AnimaName);
      _tween1=  DOVirtual.DelayedCall (AudioTime,() => { AudioManager.Instance.AudioPlay (AudioType.Boss_Borther_Attack); },false);

        _tween = DOVirtual.DelayedCall (AttackOverTime,() =>
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
        _tween?.Kill ();
        _tween1?.Kill ();
    }

}
