
using UnityEngine;

namespace Script.Universal
{
    public class AttackHit: MonoBehaviour
    {
        private enum AttacksWhat { Enemy, Player };
        [SerializeField] private AttacksWhat attacksWhat;
        [SerializeField] private int TargetSide;
        [SerializeField] private GameObject OBBase;
        [SerializeField] private bool IsShootAttack;

        //AttackData 用于数据分离，DamageData 用于传递伤害参数
        public AttackData attackData;
        private DamageData damageData;


        private void Start ()
        {
            damageData = new DamageData ();
      
        }


        private void OnTriggerStay2D (Collider2D col)
        {
            Debug.Log (col.name);
            if (col.transform.position.x < OBBase.transform.position.x)
            {
                TargetSide = -1;
            }
            else
            {
                TargetSide = 1;
            }

            damageData.TargetSide = TargetSide;
            damageData.Damage = attackData.Damage;
            damageData.ShieldDamage = attackData.ShieldDamage;
            damageData.DamageMultiply = attackData.DamageMultiply;
            damageData.WhoIsAttacker = OBBase;
            damageData.beatForce = attackData.beatForce;

            //判断受伤者是谁
            if (attacksWhat == AttacksWhat.Player)
            {
                if (col.GetComponent<PlayerCharacter> () != null)
                {
                    PlayerCharacter.Instance.GetHurt (damageData);
                    //如果有自杀型敌人，则此处应该调用自杀函数
                    if(IsShootAttack)
                        Destroy(gameObject);
                    
                }
            }
            else if (attacksWhat == AttacksWhat.Enemy && col.GetComponent<EnemyCharacter> () != null)
            {
                col.GetComponent<EnemyCharacter>().GetHurt (damageData);
            }

            //todo:如果是可破坏类型（草，箱子等等）则应该调用破坏状态函数
            //todo:如果是自杀类型的敌人，则此处应该调用其受伤直接死亡函数

        }


    }
}
