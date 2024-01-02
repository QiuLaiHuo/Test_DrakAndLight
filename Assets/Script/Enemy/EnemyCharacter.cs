
using System.Collections;

using UnityEngine;
using UnityEngine.Events;

public enum EnemieState { Defence, Default }
public class EnemyCharacter: MonoBehaviour
{
    [Header ("基础属性")]
    public int CurrentHealth;
    public int CurrentShield;
    public float CurrentRecoverTime;
    public float CurrntInvincible;
    public bool Isinvincible;
    public bool IsBreak;
    public bool IsDeath;
    public bool IsPassivity = false;
    public bool IsBack;



    protected float BackStartTime;
    protected float BackDuration;
    protected Vector2 Backvector;


    [SerializeField] protected CharacterData CharacterData;
    [SerializeField] protected GameObject shield;
    [SerializeField] protected GameObject effects;
    protected Rigidbody2D rd;
    protected EnemieState state;








    public UnityEvent OnDamage;
    public UnityEvent Ondeath;
    public UnityEvent OnBreak;



    protected virtual void Start ()
    {

        rd = GetComponent<Rigidbody2D> ();
        CurrentHealth = CharacterData.Health;
        CurrentShield = CharacterData.Shield;
        BackDuration = CharacterData.backDurationTime;
        CurrentRecoverTime = 0;
        state = EnemieState.Default;

    }

    protected virtual void Update ()
    {

        CheckInvincibleTime ();

        CheckRecoverTime ();


    }




    //新受伤函数
    public virtual void GetHurt (DamageData damage)
    {
        if (Isinvincible || IsDeath)
            return;

        switch (state)
        {

            case EnemieState.Defence:
            //调用防御函数

            DamageShield (1);
            TriggerInvincible ();
            break;
            case EnemieState.Default:
            //受伤函数
            if (IsBreak)
                Damage (damage.Damage * damage.DamageMultiply);
            else
                DamageShield (damage.Damage);
            TriggerInvincible ();
            break;
        }
    }




    //触发无敌时间
    protected void TriggerInvincible ()
    {
        if (!Isinvincible)
        {
            Isinvincible = true;
            CurrntInvincible = CharacterData.InvincibleTime;
        }

    }

   
    protected void Damage (int Damage)
    {
        if (!Isinvincible)
        {
            if (CurrentHealth - Damage > 0)
            {
                CurrentHealth -= Damage;
                OnDamage?.Invoke ();
                //todo:Hurt特效
            }
            else
            {

                CurrentHealth = 0;
                IsDeath = true;
                Ondeath?.Invoke ();
            }
        }
    }

    //恢复计时器
    protected void CheckRecoverTime ()
    {
        if (IsBreak)
        {
            shield.SetActive (false);
            CurrentRecoverTime -= Time.deltaTime;
            if (CurrentRecoverTime <= 0)
            {
                CurrentRecoverTime = 0;
                IsBreak = false;
                CurrentShield = CharacterData.Shield;
                shield.SetActive (true);
            }


        }

    }

    //无敌计时器
    protected void CheckInvincibleTime()
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

    protected void DamageShield (int damage)
    {

        if (CurrentShield - damage <= 0)
        {
            //进入Break，包括动画
            OnBreak?.Invoke ();         
            CurrentShield = 0;
            CurrentRecoverTime = CharacterData.RecoverTime;
            IsBreak = true;

        }
        else
        {
            CurrentShield -= damage;
            StartCoroutine (ShieldHurt ());
        }

    }

    IEnumerator ShieldHurt ()
    {
        shield.SetActive (true);
        shield.GetComponent<SpriteRenderer> ().color = Color.red;
        yield return new WaitForSeconds (0.1f);
        shield.GetComponent<SpriteRenderer> ().color = Color.white;
        yield return new WaitForSeconds (0.1f);
        shield.GetComponent<SpriteRenderer> ().color = Color.red;
        yield return new WaitForSeconds (0.1f);
        shield.GetComponent<SpriteRenderer> ().color = Color.white;
    }

}
