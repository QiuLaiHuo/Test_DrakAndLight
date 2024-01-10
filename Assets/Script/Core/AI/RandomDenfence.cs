using BehaviorDesigner.Runtime;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Core.AI;
    
    [TaskCategory("BrotherConditional")]
    [TaskDescription("检测玩家攻击状态")]
    public class RandomDenfence : EnemyConditionals
    {
        [BehaviorDesigner.Runtime.Tasks.Tooltip("The chance that the task will return success")]
        public SharedFloat successProbability = 0.5f;
        //[BehaviorDesigner.Runtime.Tasks.Tooltip("Seed the random number generator to make things easier to debug")]
       // public SharedInt seed;
        [BehaviorDesigner.Runtime.Tasks.Tooltip("Do we want to use the seed?")]
        public SharedBool useSeed;

        protected bool TargetIsAttack;
        
        public override void OnAwake()
        {
            base.OnAwake();
            if (useSeed.Value) {
                Random.InitState((int)System.DateTime.Now.Ticks);
            }
        }

        public override TaskStatus OnUpdate()
        {
            TargetIsAttack = PlayerCharacter.Instance.state == State.Attack ? true : false;
            
            float randomValue = Random.value;
            if (randomValue <= successProbability.Value&&TargetIsAttack) {
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }

        public override void OnReset()
        {
            // Reset the public properties back to their original values
            successProbability = 0.5f;
           
            useSeed = false;
        }
    }
