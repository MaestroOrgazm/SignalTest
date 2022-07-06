using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Trigger))]

public class Signal : MonoBehaviour
{
    private float _deltaVolume = 0.25f;
    private AudioSource _audioSource;
    private Trigger _trigger;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _trigger = GetComponent<Trigger>();
        AddListener();
    }

    private void AddListener()
    {
        _trigger.OnHouseEntrance += ControlCoroutine;
    }

    private void ControlCoroutine(bool isEntrance)
    {
        if (isEntrance == true)
        {
            StopAllCoroutines();
            StartCoroutine(PlayAudio());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(StopAudio());
        }
    }

    private IEnumerator PlayAudio()
    {
        var _whiteSecond = new WaitForSeconds(1f);

        _audioSource.Play();

        while (_audioSource.volume < 1)
        {
            _audioSource.volume += _deltaVolume;
            yield return _whiteSecond;
        }
    }

    private IEnumerator StopAudio()
    {
        var _whiteSecond = new WaitForSeconds(1f);

        while (_audioSource.volume > 0)
        {
            _audioSource.volume -= _deltaVolume;

            if (_audioSource.volume <= 0)
            {
                _audioSource.Stop();
            }
            yield return _whiteSecond;
        }
    }
}
