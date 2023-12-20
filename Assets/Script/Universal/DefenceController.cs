using UnityEngine;
using UnityEngine.Events;

public class DefenceController: MonoBehaviour
{
   
    public Character character;

    private void OnEnable ()
    {
        character.state = State.Defence;
    }

    private void FixedUpdate ()
    {
        character.state = State.Defence;
    }

    private void OnDisable ()
    {
        character.state = State.Default;
    }
}
