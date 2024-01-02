using UnityEngine;

namespace BehaviorDesigner.Runtime
{
    [System.Serializable]
    public class SharedCharacterData: SharedVariable<CharacterData>
    {
        public static implicit operator SharedCharacterData (CharacterData value) { return new SharedCharacterData { mValue = value }; }
    }
}