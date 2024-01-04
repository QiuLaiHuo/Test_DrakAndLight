using BehaviorDesigner.Runtime.Tasks;




namespace Core.AI
{
    [TaskCategory ("BrotherAction")]
    [TaskDescription ("Break状态的持续，等待恢复")]
    public class BreakLoop: EnemyAction
    {
       
        public string OverAnimaName;
       

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