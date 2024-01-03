using BehaviorDesigner.Runtime.Tasks;

using DG.Tweening;


namespace Core.AI
{
    [TaskCategory ("BrotherAction")]
    [TaskDescription ("Break状态的持续，等待恢复")]
    public class BreakLoop: EnemyAction
    {
        //private Tween tween;
        // private bool Over;
        public string OverAnimaName;
        //public string AnimaName;

        public override void OnStart ()
        {
            anim.SetBool (OverAnimaName,true);
        }

        public override TaskStatus OnUpdate ()
        {
            if (!character.IsBreak)
            {
                anim.SetBool (OverAnimaName,false);
                return TaskStatus.Success;
            }

            return TaskStatus.Running;
        }

        
    }
}