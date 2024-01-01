using System.Collections;

using DG.Tweening;

using UnityEngine;
using UnityEngine.Events;

public enum State { Porfect, Defence, Default }
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
    public bool ISBack;

    private float BackStartTime;
    private float BackDuration;
    private Vector2 backvector;
    //todo:受伤时被击飞（需要禁用Move方法），以及黑影特效

    private Rigidbody2D rd;

    private Tween tween;
    public GameObject shield;
    public State state;
    public ParticleSystem Effects;
    [SerializeField] private CharacterData characterData;


    public UnityEvent OnDamage;
    public UnityEvent Ondeath;
    public UnityEvent OnBreak;



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
        //attacker = null;
        CurrentHealth = characterData.Health;
        CurrentShield = characterData.Shield;
        BackDuration = characterData.backDurationTime;
        CurrentRecoverTime = 0;
        state = State.Default;
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
                CurrentShield = characterData.Shield;
            }

        }

        CheckBackTime ();
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
            if (Effects != null)
                Effects.Play ();
            TriggerInvincible ();
            damage.WhoIsAttacker.GetComponent<EnemyController> ()?.PassivityDamage ();
            break;


            case State.Defence:
            //调用防御函数,减少护盾
            DamageShield (damage.ShieldDamage);
            TriggerInvincible ();
            ISBack = true;
            BackStartTime = Time.time;
            rd.AddForce (backvector,ForceMode2D.Impulse);
            break;
            case State.Default:
            //受伤函数
            if (IsBreak)
                Damage (damage.Damage * damage.DamageMultiply);

            else
                Damage (damage.Damage);
            TriggerInvincible ();


            ISBack = true;
            BackStartTime = Time.time;
            rd.AddForce (backvector,ForceMode2D.Impulse);



            break;
        }
    }

    private void CheckBackTime ()
    {
        if (Time.time >= BackStartTime + BackDuration)
        {
            ISBack = false;
        }
    }


    //触发无敌时间
    protected void TriggerInvincible ()
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

        if (CurrentShield - damage <= 0)
        {
            //进入Break，包括动画
            OnBreak?.Invoke ();
            CurrentShield = 0;
            CurrentRecoverTime = characterData.RecoverTime;
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


    private void OnDisable ()
    {
        tween?.Kill ();
    }
}
