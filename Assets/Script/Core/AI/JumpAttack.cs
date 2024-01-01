using BehaviorDesigner.Runtime.Tasks;

using Core.AI;

using DG.Tweening;

using UnityEngine;

public class JumpAttack: EnemyAction
{
    public float DownForce;
    public float Force;
    private bool IsGound = false;
    public float JumpDownTime;
    public string AnimName;
    // public string AttackOverName;

    private Tween tween;
    public override void OnStart ()
    {
        

        tween = DOVirtual.DelayedCall (JumpDownTime,() =>
                {

                    anim.SetTrigger (AnimName);
                    rd.AddForce (new Vector2 (rd.velocity.x,rd.velocity.y * DownForce),ForceMode2D.Impulse);
                    //IsGound = true;
                    camera.GenerateImpulse ();
                    IsGound  = true;

                },false);
    }

    public override TaskStatus OnUpdate ()
    {
       // sprit.flipX = Target.Value.transform.position.x > transform.position.x ? true : false;
        if (IsGound)
            return TaskStatus.Success;
        else return TaskStatus.Running;
    }

    public override void OnEnd ()
    {
        var dir = Target.Value.transform.position.x < transform.position.x ? -1 : 1;
        rd.AddForce (new Vector2 (dir * Force,0),ForceMode2D.Impulse);
        tween?.Kill ();
        IsGound = false;

    }
}
