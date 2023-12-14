using UnityEngine;
using UnityEngine.Events;

public class Character: MonoBehaviour
{
    [Header ("基础属性")]
    public int Health;
    public int CurrentHealth;
    public int Shield;
    public int CurrentShield;
    public float InvincibleTime;
    public float CurrntInvincible;
    public bool Isinvincible;
    public bool IsBreak;
    public bool IsDeath;

    public UnityEvent OnDamage;
    public UnityEvent Ondeath;

    private void Start ()
    {
        CurrentHealth = Health;
        CurrentShield = Shield;
    }

    private void Update ()
    {
        if (Isinvincible)
        {
            CurrntInvincible -= Time.deltaTime;
            if (CurrntInvincible <= 0)
            {
                CurrntInvincible = 0;
                Isinvincible = false;
            }
        }
    }
    public void OnTakeDamage (AttackControll attcker)
    {
        if (Isinvincible||IsDeath)
            return;

        TriggerInvincible ();
        //护盾系统判定

        //伤害判定
        if (CurrentHealth - attcker.Damage > 0)
        {
            CurrentHealth -= attcker.Damage;
            OnDamage?.Invoke ();
        }
        else
        {
            CurrentHealth = 0;
            IsDeath = true;
            Ondeath?.Invoke ();
        }


    }


    //触发无敌时间
    private void TriggerInvincible ()
    {
        if (!Isinvincible)
        {
            Isinvincible = true;
            CurrntInvincible = InvincibleTime;
        }

    }

    public void TriggerShield ()
    {

    }

}
