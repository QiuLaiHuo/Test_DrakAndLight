using BehaviorDesigner.Runtime;

using UnityEngine;
using UnityEngine.Events;

public enum EnemieState {  Defence, Default }
public class EnemyCharacter: MonoBehaviour
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
    public bool IsPassivity = false;
    public EnemieState state;
    //public bool IsDefence;
    //public bool IsPorfectDefence;
    public ParticleSystem Effects;
    //private Controller controller;
    // public bool Flip = true;
    //[SerializeField]


    public UnityEvent OnDamage;
    public UnityEvent Ondeath;
    public UnityEvent OnBreak;
    // public UnityEvent OnPorfect;

   // protected AttackControll attacker;


    private void Start ()
    {

        //attacker = null;
        CurrentHealth = Health;
        CurrentShield = Shield;
        CurrentRecoverTime = 0;
        state = EnemieState.Default;
        //controller = GetComponent<Controller> ();
    }

    private void Update ()
    {
        // Debug.Log (state);
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


    //�����˺���
    public void GetHurt (DamageData damage)
    {
        if (Isinvincible || IsDeath)
            return;

        switch (state)
        {
            
            case EnemieState.Defence:
            //���÷�������

            DamageShield (damage.ShieldDamage);
            break;
            case EnemieState.Default:
            //���˺���

            if (IsBreak)
                Damage (damage.Damage * Multiply);
            else
                Damage (damage.Damage);
            TriggerInvincible ();
            break;
        }
    }


    //�����˺���
    //public virtual void OnTakeDamage (AttackControll attack)
    //{
    //    if (Isinvincible || IsDeath)
    //        return;
    //    this.attacker = attack;


    //    switch (state)
    //    {
    //        case State.Porfect:
    //        //���ù����߱�����
    //        //OnPorfect?.Invoke();
    //        if (Effects != null)
    //            Effects.Play ();
    //        TriggerInvincible ();
    //        damage.WhoIsAttacker.GetComponent<EnemyController> ()?.PassivityDamage ();
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


    //�����޵�ʱ��
    protected void TriggerInvincible ()
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
    protected void Damage (int Damage)
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



    protected void DamageShield (int damage)
    {
        if ( CurrentShield <= 0)
            return;
        if (CurrentShield - damage <= 0)
        {
            //����Break����������
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
