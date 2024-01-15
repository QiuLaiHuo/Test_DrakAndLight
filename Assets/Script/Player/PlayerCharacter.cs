
using System.Collections;

using DG.Tweening;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public enum State { Porfect, Defence, Default ,Attack}
public class PlayerCharacter: MonoBehaviour
{
    [Header ("基础属性")]
    public int CurrentHealth;
    public int CurrentShield;
    public float CurrentRecoverTime;
    public float CurrntInvincible;
    public bool Isinvincible;
    public bool IsBreak;
    public bool IsDeath;
    [FormerlySerializedAs("ISBack")] public bool isBack;

    private float BackStartTime;
    private float BackDuration;
    private Vector2 backvector;
    

    private Rigidbody2D rd;

    private Tween tween;
    public GameObject shield;
    public State state;

    [SerializeField] private CharacterData characterData;
   // [SerializeField] private ParticleSystem ProfectDefence;

    public UnityAction OnDamage;
    public UnityAction Ondeath;
    public UnityAction OnShieldChange;
    public UnityAction OnLifeChange;



    private static PlayerCharacter instance;
    public static PlayerCharacter Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindAnyObjectByType<PlayerCharacter> ();

            return instance;
        }
    }

    private void Start ()
    {
        rd = GetComponent<Rigidbody2D> ();

        CurrentHealth = characterData.Health;
        CurrentShield = characterData.Shield;
        BackDuration = characterData.backDurationTime;
        CurrentRecoverTime = 0;
        state = State.Default;
        Player_UI.OnShieldChange += () => CurrentShield;
        Player_UI.OnLifeChange += () => CurrentHealth;
        OnShieldChange.Invoke();
        OnLifeChange.Invoke();
    }

  
    private void Update ()
    {
        CheckInvincibleTime ();
        CheckRecoverTime ();
        CheckBackTime ();
    }


    //无敌计时器
    private void CheckInvincibleTime ()
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

    //恢复计时器
    private void CheckRecoverTime ()
    {
        if (IsBreak&&!IsDeath)
        {

            CurrentRecoverTime -= Time.deltaTime;
            if (CurrentRecoverTime <= 0)
            {
                CurrentRecoverTime = 0;
                IsBreak = false;
                CurrentShield = characterData.Shield;
                OnShieldChange.Invoke();
            }

        }

    }

    private void PorfectShield()
    {
        if (CurrentShield < characterData.Shield)
        {
            CurrentShield++;
            OnShieldChange.Invoke();
        }
        
    }
    
    //新受伤函数
    public void GetHurt (DamageData damage)
    {
        if (Isinvincible || IsDeath)
            return;

        backvector.Set (damage.beatForce.x * damage.TargetSide,damage.beatForce.y);

        switch (state)
        {
            case State.Porfect:

            TriggerInvincible ();
            PorfectShield();
             TimeManager.Instance.SlowTime ();

            backvector.Set(damage.beatForce.x * -damage.TargetSide,damage.beatForce.y);
            damage.WhoIsAttacker.GetComponent<EnemyController>()?.PassivityToTreeEvent(
                damage.ShieldDamage * damage.DamageMultiply,
                backvector);
           
            break;


            case State.Defence:
            //调用防御函数,减少护盾
            DamageShield (damage.ShieldDamage);
            TriggerInvincible ();

            isBack = true;
            BackStartTime = Time.time;
            rd.AddForce (backvector,ForceMode2D.Impulse);
            TimeManager.Instance.SlowTime ();
            break;
            default:
            //受伤函数
            if (IsBreak)
                Damage (damage.Damage * damage.DamageMultiply);

            else
                Damage (damage.Damage);
            TriggerInvincible ();

        
            isBack = true;
            BackStartTime = Time.time;
            rd.AddForce (backvector,ForceMode2D.Impulse);
            TimeManager.Instance.SlowTime ();
            break;
        }
    }

    private void CheckBackTime ()
    {
        if (Time.time >= BackStartTime + BackDuration)
        {
            isBack = false;
        }
    }


    //触发无敌时间
    private void TriggerInvincible ()
    {
        if (!Isinvincible)
        {
            Isinvincible = true;
            CurrntInvincible = characterData.InvincibleTime;
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
            OnLifeChange.Invoke();
        }
    }



    private void DamageShield (int damage)
    {

        if (CurrentShield - damage <= 0)
        {
            //todo:进入Break，包括动画
           // OnBreak?.Invoke ();
            CurrentShield = 0;
           
            CurrentRecoverTime = characterData.RecoverTime;
            IsBreak = true;

        }
        else
        {
            AudioManager.Instance.AudioPlay (AudioType.PlayerDefence);
            CurrentShield -= damage;
            StartCoroutine (ShieldHurt ());
        }
        OnShieldChange.Invoke();

    }

    IEnumerator ShieldHurt ()
    {
        //shield.SetActive (true);
        shield.GetComponent<SpriteRenderer> ().color = Color.red;
        yield return new WaitForSeconds (0.1f);
        shield.GetComponent<SpriteRenderer> ().color = Color.white;
        yield return new WaitForSeconds (0.1f);
        shield.GetComponent<SpriteRenderer> ().color = Color.red;
        yield return new WaitForSeconds (0.1f);
        shield.GetComponent<SpriteRenderer> ().color = Color.white;
    }


    private void OnDisable ()
    {
        tween?.Kill ();
    }




}
