using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;

namespace Core.AI
{
    [TaskCategory("BrotherAction")]
    [TaskDescription("Boss进入Break状态时行为")]
    public class Break : EnemyAction
    {
        public string AnimaName;
        private Tween tween;
        public float OverTime;
        private bool Over;
        public override void OnStart()
        {
            anim.SetBool(AnimaName,true);

            tween = DOVirtual.DelayedCall(OverTime, () =>
                {
                    Over = true;
                }, false
            );
        }


        public override TaskStatus OnUpdate()
        {
            if (Over)
                return TaskStatus.Success;
            return TaskStatus.Running;
        }


        public override void OnEnd()
        {
            Over = false;
            tween?.Kill();
        }
    }
}