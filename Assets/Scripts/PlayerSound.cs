using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _deathAudio;
    [SerializeField]
    private AudioClip _movementAudio;
    [SerializeField]
    private AudioClip _ateFoodAudio;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayDeathAudio()
    {
        _audioSource.Stop();
        _audioSource.clip = _deathAudio;
        _audioSource.Play();
    }

    public void PlayMovementAudio()
    {
        _audioSource.Stop();
        _audioSource.clip = _movementAudio;
        _audioSource.Play();
    }

    public void PlayAteFoodAudio()
    {
        _audioSource.Stop();
        _audioSource.clip = _ateFoodAudio;
        _audioSource.Play();
    }
}
