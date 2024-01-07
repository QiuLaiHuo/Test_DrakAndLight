using DG.Tweening;

using UnityEngine;
using UnityEngine.Rendering.Universal;

using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Shield: MonoBehaviour
{


    private Light2D sprite;
    private bool ToDoOver = false;
    [SerializeField] float BreatheTime = 1.5f;
    private bool Check1=false, Check2=false;
    void Start ()
    {
        DOTween.Init (true,true,LogBehaviour.Verbose).SetCapacity (1000,200);
        sprite = GetComponent<Light2D> ();
    }


    void Update ()
    {
        if (ToDoOver&&!Check2)
        {
            ShieldBreatheToOne ();
        }
        else if(!ToDoOver&&!Check1)
            ShieldBreatheTopointfive ();
    }


    private void ShieldBreatheTopointfive ()
    {
        Check1 = true;
      DOTween.To (() => sprite.intensity,x => sprite.intensity = x,0f,BreatheTime);
       
       DOVirtual.DelayedCall ( BreatheTime,()=>
        {
            ToDoOver = true;
            Check1 = false;
        }     
        ,false);

    }

    private void ShieldBreatheToOne ()
    {
        Check2 = true;
       DOTween.To (() => sprite.intensity,x => sprite.intensity = x,5f,BreatheTime);
       DOVirtual.DelayedCall (BreatheTime,() =>
        {
            ToDoOver = false;
            Check2 = false;
        }
        ,false);
    }


   
}
