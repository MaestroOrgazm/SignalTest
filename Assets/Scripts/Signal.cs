using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal : MonoBehaviour
{
    private float _entranceCount = 0f;
    private float _deltaVolume = 0.003f;
    private AudioSource _audioSource;
    private bool _isPlay = false;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Walk>(out Walk walk))
        {
            _entranceCount++;
        }
    }

    private void Update()
    {
        if (_entranceCount > 0)
        {
            if (_entranceCount % 2 > 0)
            {
                PlayAudio();
                _audioSource.volume += _deltaVolume;

                if (_audioSource.volume == 1)
                {
                    _audioSource.volume = 1;
                }
            }
            else
            {
                _audioSource.volume -= _deltaVolume;

                if (_audioSource.volume == 0)
                {
                    _audioSource.volume = 0;
                    StopAudio();
                }
            }
        }
    }

    private void PlayAudio()
    {
        if (_isPlay == false)
        {
            _audioSource.Play();
            _isPlay = true;
        }
    }

    private void StopAudio()
    {
        if (_isPlay == true)
        {
            _audioSource.Stop();
            _isPlay= false;
        }
    }
}
