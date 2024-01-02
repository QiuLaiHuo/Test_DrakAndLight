
using UnityEngine;
using UnityEngine.Events;

public enum EnemieState {  Defence, Default }
public class EnemyCharacter: MonoBehaviour
{
    [Header ("基础属性")]
    public int Health;
    public int CurrentHealth;
    public int Shield;
    public int CurrentShield;
    public float RecoverTime;
    public float CurrentRecoverTime;
    public float InvincibleTime;
    public float CurrntInvincible;
    public int Multiply;
    public bool Isinvincible;
    public bool IsBreak;
    public bool IsDeath;
    public bool IsPassivity = false;
    public EnemieState state;
   
    public ParticleSystem Effects;
   


    public UnityEvent OnDamage;
    public UnityEvent Ondeath;
    public UnityEvent OnBreak;
   


    private void Start ()
    {

        
        CurrentHealth = Health;
        CurrentShield = Shield;
        CurrentRecoverTime = 0;
        state = EnemieState.Default;
       
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

        if (IsBreak)
        {
            CurrentRecoverTime -= Time.deltaTime;
            if (CurrentRecoverTime <= 0)
            {
                CurrentRecoverTime = 0;
                IsBreak = false;
                CurrentShield = Shield;
            }

        }
    }


    //新受伤函数
    public void GetHurt (DamageData damage)
    {
        if (Isinvincible || IsDeath)
            return;

        switch (state)
        {
            
            case EnemieState.Defence:
            //调用防御函数

            DamageShield (damage.ShieldDamage);
            break;
            case EnemieState.Default:
            //受伤函数

            if (IsBreak)
                Damage (damage.Damage * Multiply);
            else
                Damage (damage.Damage);
            TriggerInvincible ();
            break;
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

    public void DodgeInvincible (float Time)
    {
        Isinvincible = true;
        CurrntInvincible = Time;
    }
    private void Damage (int Damage)
    {
        if (!Isinvincible)
        {
            if (CurrentHealth - Damage > 0)
            {
                CurrentHealth -= Damage;
                OnDamage?.Invoke ();
            }
            else
            {

                CurrentHealth = 0;
                IsDeath = true;
                Ondeath?.Invoke ();
            }
        }
    }



    private void DamageShield (int damage)
    {
        if ( CurrentShield <= 0)
            return;
        if (CurrentShield - damage <= 0)
        {
            //进入Break，包括动画
            OnBreak?.Invoke ();
            CurrentShield = 0;
            CurrentRecoverTime = RecoverTime;
            IsBreak = true;

        }
        else
        {
            CurrentShield -= damage;
        }

    }


}
