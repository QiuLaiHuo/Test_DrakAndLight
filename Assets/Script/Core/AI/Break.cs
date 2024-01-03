using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;

using UnityEngine;

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



            TimeManager.Instance.SlowTime ();
            anim.SetTrigger (AnimaName);
           
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