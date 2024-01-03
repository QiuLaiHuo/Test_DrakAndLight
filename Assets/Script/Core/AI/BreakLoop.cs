using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;


namespace Core.AI
{
    [TaskCategory("BrotherAction")]
    [TaskDescription("Break状态的持续，等待恢复")]
    public class BreakLoop:EnemyAction
    {
        private Tween tween;
        private bool Over;

//todo:可直接从Character中调用Break状态判断
        public override void OnStart()
        {
            tween = DOVirtual.DelayedCall(CharacterData.Value.RecoverTime, () =>
            {
                //todo:结束动画状态
                Over = true;
            }, false);
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