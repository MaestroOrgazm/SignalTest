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
    private Coroutine _changeAudio;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(1f);

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
        _trigger = GetComponent<Trigger>();
        _trigger.EntryHouse += ChoiceVolume;
    }

    private void OnDisable()
    {
        _trigger.EntryHouse -= ChoiceVolume;
    }

    private void ChoiceVolume(bool isEntrance)
    {
        if (isEntrance)
        {
            _audioSource.Play();
            PlayAudio(_maxVolume);
            return;
        }
        PlayAudio(_minVolume);
    }

    private void PlayAudio(float volume)
    {
        if (_changeAudio != null)
        {
            StopCoroutine(_changeAudio);
        }

        _changeAudio = StartCoroutine(ChangeVolume(volume));
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _deltaVolume * Time.deltaTime);
            yield return _waitForSeconds;
        }
    }
}
