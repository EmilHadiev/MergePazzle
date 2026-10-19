using CartoonFX;
using UnityEngine;

public class ParticleTextView : ParticleView
{
    [SerializeField] private CFXR_ParticleText _particleText;

    private void OnValidate()
    {
        _particleText ??= GetComponent<CFXR_ParticleText>();
    }

    public void ShowText(string text)
    {
        _particleText.UpdateText(text);
    }
}