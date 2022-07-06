using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Trigger))]

public class Signal : MonoBehaviour
{
    private float _deltaVolume = 15f;
    private AudioSource _audioSource;
    private Trigger _trigger;
    private Coroutine _playAudio;
    private Coroutine _stopAudio;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(1f);

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
            if (_stopAudio != null)
            {
                StopCoroutine(_stopAudio);
            }

            _audioSource.Play();
            _playAudio = StartCoroutine(PlayAudio());
        }
        else
        {
            if (_playAudio != null)
            {
                StopCoroutine(_playAudio);
            }

            _stopAudio = StartCoroutine(StopAudio());
        }
    }

    private IEnumerator PlayAudio()
    {

        while (_audioSource.volume != _maxVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume,_maxVolume, _deltaVolume * Time.deltaTime );
            yield return _waitForSeconds;
        }
    }

    private IEnumerator StopAudio()
    {
        while (_audioSource.volume != _minVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _minVolume, _deltaVolume * Time.deltaTime);
            yield return _waitForSeconds;
        }

        _audioSource.Stop();
    }
}
