using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;


namespace Core.AI
{
    [TaskCategory("BrotherAction")]
    public class Defence : EnemyAction
    {
        public string AnimName;
        public float OverTime;
        private bool Over=false;
        private Tween _tween;

        public override void OnStart()
        {
           
            anim.SetBool(AnimName, true);
            character.state = EnemieState.Defence;
          _tween =  DOVirtual.DelayedCall(OverTime, () =>
            {
                Over = true;
           
            
        }, false);
            
            
        }

        public override TaskStatus OnUpdate()
        {
            if (Over)
               return TaskStatus.Success;
            return   TaskStatus.Running;
            
        }

        public override void OnEnd()
        {
            Over = false;
            character.state = EnemieState.Default;
            anim.SetBool(AnimName, false);
            _tween?.Kill();
        }
    }
}