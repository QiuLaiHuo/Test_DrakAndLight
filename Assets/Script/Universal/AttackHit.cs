using UnityEngine;

public class AttackHit: MonoBehaviour
{
    public enum AttacksWhat { Enemy, Player };
    [SerializeField] private AttacksWhat attacksWhat;
    [SerializeField] private int TargetSide;
    [SerializeField] private GameObject OBBase;


    //AttackData �������ݷ��룬DamageData ���ڴ����˺�����
    public AttackData attackData;
    private DamageData damageData;


    private void Start ()
    {
        damageData = new DamageData ();
      
    }


    private void OnTriggerStay2D (Collider2D col)
    {
        Debug.Log (3);
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

        //�ж���������˭
        if (attacksWhat == AttacksWhat.Player)
        {
            if (col.GetComponent<PlayerCharacter> () != null)
            {
                PlayerCharacter.Instance.GetHurt (damageData);
                //�������ɱ�͵��ˣ���˴�Ӧ�õ�����ɱ����
            }
        }

        else if (attacksWhat == AttacksWhat.Enemy && col.GetComponent<EnemyCharacter> () != null)
        {
            col.GetComponent<EnemyCharacter>().GetHurt (damageData);
        }

        //todo:����ǿ��ƻ����ͣ��ݣ����ӵȵȣ���Ӧ�õ����ƻ�״̬����
        //todo:�������ɱ���͵ĵ��ˣ���˴�Ӧ�õ���������ֱ����������

    }


}
