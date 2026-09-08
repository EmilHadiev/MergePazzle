using UnityEngine;

public class CharacterEffectViewer : MonoBehaviour
{
    private IPassiveCharacterEffect[] _passiveEffects;

    private void Start()
    {
        _passiveEffects = GetComponents<IPassiveCharacterEffect>();

        for (int i = 0; i < _passiveEffects.Length; i++)
            _passiveEffects[i].Play();
    }
}
