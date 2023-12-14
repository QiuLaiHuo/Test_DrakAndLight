using UnityEngine;
using UnityEngine.Events;

public class Character: MonoBehaviour
{
    [Header ("��������")]
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
        //����ϵͳ�ж�

        //�˺��ж�
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


    //�����޵�ʱ��
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
