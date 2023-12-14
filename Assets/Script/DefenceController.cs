using UnityEngine;
using UnityEngine.Events;

public class DefenceController: MonoBehaviour
{
    public float DefenceTimeCD;
    [SerializeField]
    private float CurrentDefenceTime;
    bool IsDefence;

    public UnityEvent Defence;

    private void Start ()
    {
        CurrentDefenceTime = 0;
    }
    private void Update ()
    {
        if (IsDefence)
        {

            CurrentDefenceTime -= Time.deltaTime;
            if (CurrentDefenceTime <= 0)
            {
                CurrentDefenceTime = 0;
                IsDefence = false;
            }

        }
    }

    public void OnDefence (AttackControll attack)
    {
        if (!IsDefence)
        {
            IsDefence = true;
            CurrentDefenceTime = DefenceTimeCD;
            Defence?.Invoke ();
        }
       // Debug.Log (1);

    }
}
