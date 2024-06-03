using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance { get; private set; }

    private AudioSource source;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        source = GetComponent<AudioSource>();
        source.volume = 0f;
    }

    public void StartPlaying()
    {
        VolumeFade(1f, 1f, onStart: source.Play);
    }

    public void StopPlaying()
    {
        VolumeFade(1f, 0f, onComplete: source.Stop);
    }

    public void VolumeFade(float duration, float targetVolume, Action onStart = null, Action onComplete = null)
    {
        StartCoroutine(VolumeFade_(duration, targetVolume, onStart, onComplete));
    }

    private IEnumerator VolumeFade_(float duration, float targetVolume, Action onStart, Action onComplete)
    {
        onStart?.Invoke();

        float timer = 0f;
        float startVolume = source.volume;

        while (timer < duration)
        {
            source.volume = Mathf.Lerp(startVolume, targetVolume, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }

        onComplete?.Invoke();
    }
}
