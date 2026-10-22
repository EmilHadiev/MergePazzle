using System;
using UnityEngine;

[Serializable]
public struct SoundEntry
{
    [field: SerializeField] public SoundNames Name { get; private set; }
    [field: SerializeField] public AudioClip Clip { get; private set; }
}