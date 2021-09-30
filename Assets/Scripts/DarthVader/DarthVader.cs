using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DarthVader : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_audioSource.isPlaying == false)
        {
            Destroy(gameObject);
        }
    }
}
