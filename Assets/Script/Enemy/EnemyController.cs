using System;
using BehaviorDesigner.Runtime;

using Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

using Random = UnityEngine.Random;

public class EnemyController: MonoBehaviour
{

    private BehaviorTree tree;
   
    private CinemachineImpulseSource cinema;
    private Animator anim;
    private Rigidbody2D rd;
    private bool IsTargetDie=false;
    public int facingDirection = -1;
   // private int curretfacing;
    
    [SerializeField] private float CheckRange;
    [SerializeField] private LayerMask CheckWhat;
    [SerializeField] private ParticleSystem Parry;
    [SerializeField] private float FlySpeed;
    [SerializeField] private GameObject ShootAttack;
    [SerializeField] private GameObject ShootPosition;
    [SerializeField] private float Shootlife;
    
    
    private Collider2D CheckCol;

    public static UnityAction<int,Vector2> ProfectDefence;
    public static UnityAction<int> CurrentFacing;
    
    private void Awake ()
    {
        rd = GetComponent<Rigidbody2D> ();
        tree = GetComponent<BehaviorTree> ();
        anim = GetComponent<Animator> ();
        cinema = GetComponent<CinemachineImpulseSource> ();
        
    }

    private void Start()
    {
        EnemyCharacter.OnShake += OnImpulseSource;
        PlayerCharacter.Instance.Ondeath += TargetDie;
    }

    //todo:使用GameManager来统一调用
    void TargetDie()
    {
        IsTargetDie = true;
        tree?.SendEvent("OnDie");
        
    }
    
    private void FixedUpdate ()
    {
        if(!IsTargetDie)
            CheckPlayer ();
        
    }

    
    //摄像机震动方法
    public void OnImpulseSource ()
    {
        cinema.GenerateImpulse (new Vector3 (Random.Range (-0.3f,0.3f),0f,0f));
    }

    public void Shoot()
    {
        
     GameObject a = Instantiate(ShootAttack,transform.TransformPoint(ShootPosition.transform.localPosition) 
         , quaternion.identity);
     a.GetComponent<SpriteRenderer>().flipX = facingDirection < 0 ? true : false;
        a.GetComponent<Rigidbody2D>().AddForce(new Vector2(FlySpeed*facingDirection,0),
           ForceMode2D.Force );
        Destroy(a,Shootlife);

    }
    public void PassivityToTreeEvent (int ShieldDamage,Vector2 backDirection)
    {
        tree?.SendEvent ("Onporfect");
        AudioManager.Instance.AudioPlay (AudioType.Boss_Borther_Parry);
        ProfectDefence?.Invoke (ShieldDamage,backDirection);
       Parry?.Play();
    }
    
    
    private void CheckShouldFlip ()
    {
        if (CheckCol.transform.position.x > transform.position.x && facingDirection != 1)
        {
            
            Flip ();
        }
        if (CheckCol.transform.position.x < transform.position.x && facingDirection != -1)
            Flip ();
    }

    private void Flip ()
    {
        facingDirection *= -1;
      CurrentFacing?.Invoke(facingDirection);
        rd.transform.Rotate (0f,180f,0f);
    }


    private void CheckPlayer ()
    {
        CheckCol = Physics2D.OverlapCircle (transform.position,CheckRange,CheckWhat);
        if (CheckCol != null)
        {
            CheckShouldFlip ();
        }
    }

    private void OnDrawGizmos ()
    {
        Gizmos.DrawWireSphere (transform.position,CheckRange);
    }
    //private void Update () { }

   
}
