using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RobotSoundContainer : MonoBehaviour, ISoundContainer
{
    [SerializeField] private SoundEntry[] _sounds;
    [SerializeField] private AudioSource _audioSource;

    private Dictionary<SoundNames, AudioClip> _clips;

    private void OnValidate()
    {
        _audioSource ??= GetComponent<AudioSource>();
    }

    private void Awake()
    {
        _audioSource ??= GetComponent<AudioSource>();
        BuildCache();
    }

    public void Play(SoundNames sound)
    {
        if (_clips.TryGetValue(sound, out var clip) == false)
        {
            Debug.LogWarning($"{nameof(RobotSoundContainer)}: clip for sound \"{sound}\" is not set.", this);
            return;
        }

        _audioSource.PlayOneShot(clip);
    }

    public void Stop() => _audioSource.Stop();

    private void BuildCache()
    {
        if (_sounds == null || _sounds.Length == 0)
        {
            Debug.LogError($"{nameof(RobotSoundContainer)}: sounds are not configured.", this);
            return;
        }

        _clips = new Dictionary<SoundNames, AudioClip>(_sounds.Length);

        foreach (var sound in _sounds)
        {
            if (sound.Clip == null)
            {
                Debug.LogError($"{nameof(RobotSoundContainer)}: sound \"{sound.Name}\" has no clip.", this);
                continue;
            }

            if (!_clips.TryAdd(sound.Name, sound.Clip))
                Debug.LogWarning($"{nameof(RobotSoundContainer)}: sound \"{sound.Name}\" is duplicated.", this);
        }
    }
}
