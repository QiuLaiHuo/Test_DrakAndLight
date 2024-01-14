
using System.Collections;

using BehaviorDesigner.Runtime;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

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
    protected int Facing;
    
    
    [SerializeField] protected CharacterData CharacterData;
    [SerializeField] protected GameObject shield;  
     [SerializeField] protected ParticleSystem Shieldeffects;
     [SerializeField] protected ParticleSystem HurtEffects;
      
     
     
     
    protected Rigidbody2D rd;
    public EnemieState state;
    protected BehaviorTree tree;
    protected Vector2 BackVector;



    


    public static UnityAction OnShake;
    //public UnityEvent Ondeath;
    //public UnityEvent OnBreak;



    protected virtual void Start ()
    {
        tree = GetComponent<BehaviorTree> ();
        rd = GetComponent<Rigidbody2D> ();
        CurrentHealth = CharacterData.Health;
        CurrentShield = CharacterData.Shield;
        BackDuration = CharacterData.backDurationTime;
        CurrentRecoverTime = 0;
        state = EnemieState.Default;
       
        EnemyController.ProfectDefence += DamageShield;
        EnemyController.CurrentFacing += FacingEvent;
    }

    protected virtual void Update ()
    {

        CheckInvincibleTime ();

        CheckRecoverTime ();


    }


    protected virtual void FacingEvent(int face)
    {
        Facing = face;
    }


    //新受伤函数
    public virtual void GetHurt (DamageData damage)
    {
        if (Isinvincible || IsDeath)
            return;
        
        BackVector.Set(damage.beatForce.x * damage.TargetSide,damage.beatForce.y);
       

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

            AudioManager.Instance.AudioPlay (AudioType.Boss_Borther_Hurt);
            if (CurrentHealth - Damage > 0)
            {
                CurrentHealth -= Damage;
                StartCoroutine(HeathHurt());
                  OnShake?.Invoke ();
               
                  HurtEffects.Play();
            }
            else
            {

                CurrentHealth = 0;
                IsDeath = true;
                tree?.SendEvent ("OnDie");
                shield.SetActive (false);
                //Ondeath?.Invoke ();
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
            if (CurrentRecoverTime <= 0  )
            {
                CurrentRecoverTime = 0;
                IsBreak = false;
                CurrentShield = CharacterData.Shield;
                if(!IsDeath)
                    shield.SetActive (true);
            }


        }

    }

    //无敌计时器
    protected void CheckInvincibleTime ()
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

    protected virtual void DamageShield (int damage)
    {
        if (CurrentShield - damage <= 0)
        {
            OnShake?.Invoke ();
            tree?.SendEvent ("OnBreak");
             Shieldeffects.Play();

            AudioManager.Instance.AudioPlay (AudioType.Boss_Borther_Break);
            TimeManager.Instance.SlowTime ();
           
            
            rd.AddForce (BackVector,ForceMode2D.Impulse);
            CurrentRecoverTime = CharacterData.RecoverTime;
            CurrentShield = 0;
            IsBreak = true;
        }
        else
        {
            CurrentShield -= damage;
            AudioManager.Instance.AudioPlay (AudioType.Boss_Borther_Defence);
            StartCoroutine (ShieldHurt ());
        }
    }

    protected virtual void DamageShield(int damage, Vector2 backirection)
    {
        
        
        if (CurrentShield - damage <= 0)
        {
            OnShake?.Invoke ();
            tree?.SendEvent ("OnBreak");   
             Shieldeffects.Play();
            AudioManager.Instance.AudioPlay (AudioType.Boss_Borther_Break);


            rd.AddForce (backirection,ForceMode2D.Impulse);
            CurrentRecoverTime = CharacterData.RecoverTime;
            CurrentShield = 0;
            IsBreak = true;
        }
        else
        {
            CurrentShield -= damage;
            AudioManager.Instance.AudioPlay (AudioType.Boss_Borther_Defence);
            StartCoroutine (ShieldHurt ());
        }
    }

    // IEnumerator ShieldDisappear()
    // {
    //     //
    //     shield.SetActive (true);
    // }

    IEnumerator ShieldHurt ()
    {
        SpriteRenderer sp = shield.GetComponent<SpriteRenderer>();

        if (sp == null)
            yield break; 
        sp.color = Color.red;
        yield return new WaitForSeconds (0.1f);
        sp.color = Color.white;
        yield return new WaitForSeconds (0.1f);
        sp.color = Color.red;
        yield return new WaitForSeconds (0.1f);
        sp.color = Color.white;
    }

    IEnumerator HeathHurt()
    {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        Color color = new Color(245f/255f,181f/255f,98f/255f,1f);
        if (sp == null)
            yield break;
        sp.color = color;
        yield return new WaitForSeconds (0.1f);
        sp.color = Color.white;
        yield return new WaitForSeconds (0.1f);
        sp.color = color;
        yield return new WaitForSeconds (0.1f);
        sp.color = Color.white;
        
    }
    
    
}
