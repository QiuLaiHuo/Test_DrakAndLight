using UnityEngine;
using UnityEngine.Events;

public class Character: MonoBehaviour
{
    [Header ("��������")]
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
    


    //[SerializeField]


    public UnityEvent OnDamage;
    public UnityEvent Ondeath;
    public UnityEvent OnBreak;

    private AttackControll attacker;

    private void Start ()
    {
        attacker = null;
        CurrentHealth = Health;
        CurrentShield = Shield;
        CurrentRecoverTime = 0;
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
    public void OnTakeDamage (AttackControll attack)
    {
        if (Isinvincible || IsDeath)
            return;
        this.attacker = attack;
        TriggerInvincible ();
        //����ϵͳ�ж�
        DamageShield ();

        //�˺��ж�
        if (IsBreak)
            Damage (attacker.Damage * Multiply);
        else
            Damage (attacker.Damage);
    }


    //�����޵�ʱ��
    private void TriggerInvincible ()
    {
        if (!Isinvincible)
        {
            Isinvincible = true;
            CurrntInvincible = InvincibleTime;
        }

    }

    private void Damage (int Damage)
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



    private void DamageShield ()
    {
        if (attacker == null||CurrentShield<=0)
            return;
        if (CurrentShield - attacker.Damage <= 0)
        {
            //����Break����������
            OnBreak?.Invoke ();
            CurrentShield = 0;
            CurrentRecoverTime = RecoverTime;
            IsBreak = true;

        }
        else
        {
            CurrentShield -= attacker.Damage;
        }

    }


}
