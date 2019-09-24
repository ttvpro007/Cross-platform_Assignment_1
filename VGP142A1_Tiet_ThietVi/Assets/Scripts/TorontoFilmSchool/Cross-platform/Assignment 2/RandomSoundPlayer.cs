using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] damageAudioClips;
    [SerializeField] AudioClip[] deathAudioClips;

    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
        source.volume = 0.3f;
    }

    public void PlayRandomDamageClip()
    {
        source.clip = damageAudioClips[Random.Range(0, damageAudioClips.Length)];
        Play(source.clip);
    }

    public void PlayRandomDeathClip()
    {
        source.clip = deathAudioClips[Random.Range(0, damageAudioClips.Length)];
        Play(source.clip);
    }

    private void Play(AudioClip clip)
    {
        if (source.isPlaying) return;
        source.Play();
    }
}
