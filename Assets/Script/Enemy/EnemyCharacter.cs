using System.Collections;
using System.Collections.Generic;

using BehaviorDesigner.Runtime;

using UnityEditor.Experimental.GraphView;

using UnityEngine;


public class EnemyCharacter : Character
{
    private BehaviorTree tree;

    private void Awake ()
    {
        tree = GetComponent<BehaviorTree>();
    }

    //protected override void OnTakeDamage (AttackControll attack)
    //{
    //    if (Isinvincible || IsDeath)
    //        return;
    //    this.attacker = attack;


    //    switch (state)
    //    {
    //        case State.Porfect:
    //        //���ù����߱�����
    //        tree?.SendEvent ("Onporfect");
    //        if (Effects != null)
    //            Effects.Play ();
    //        TriggerInvincible ();
    //        attack.PassivityDamage ();
    //        break;
    //        case State.Defence:
    //        //���÷�������

    //        DamageShield ();
    //        break;
    //        case State.Default:
    //        //���˺���

    //        if (IsBreak)
    //            Damage (attacker.Damage * Multiply);
    //        else
    //            Damage (attacker.Damage);
    //        TriggerInvincible ();
    //        break;
    //    }
    //}
}
