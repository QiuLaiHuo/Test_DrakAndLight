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
    //        //调用攻击者被弹刀
    //        tree?.SendEvent ("Onporfect");
    //        if (Effects != null)
    //            Effects.Play ();
    //        TriggerInvincible ();
    //        attack.PassivityDamage ();
    //        break;
    //        case State.Defence:
    //        //调用防御函数

    //        DamageShield ();
    //        break;
    //        case State.Default:
    //        //受伤函数

    //        if (IsBreak)
    //            Damage (attacker.Damage * Multiply);
    //        else
    //            Damage (attacker.Damage);
    //        TriggerInvincible ();
    //        break;
    //    }
    //}
}
