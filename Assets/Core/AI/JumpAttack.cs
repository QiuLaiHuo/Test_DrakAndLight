using BehaviorDesigner.Runtime.Tasks;

using Core.AI;

using DG.Tweening;

using UnityEngine;

public class JumpAttack: EnemyAction
{
    public float DownForce;
    private bool IsGound;
    public float JumpDownTime;
    public string AnimName;
    public string AttackOverName;

    private Tween tween;
    public override void OnStart ()
    {
        anim.SetTrigger (AnimName);
        rd.AddForce (new Vector2 (rd.velocity.x,DownForce),ForceMode2D.Impulse);
    }

    public override TaskStatus OnUpdate ()
    {
      tween =   DOVirtual.DelayedCall (JumpDownTime,() =>
        {
            IsGound = true;
            


        },false);
        return TaskStatus.Success;
    }

    public override void OnEnd ()
    {
        tween?.Kill ();
        if (IsGound)
            anim.SetTrigger (AttackOverName);

    }
}
