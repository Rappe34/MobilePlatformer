using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance { get; private set; }

    private AudioSource source;
    private AudioLowPassFilter lowPass;

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
        lowPass = GetComponent<AudioLowPassFilter>();
    }

    private void Start()
    {
        source.volume = 0f;
        lowPass.cutoffFrequency = 22000f;

        StartPlaying();
    }

    public void StartPlaying()
    {
        VolumeFade(3f, 1f, onStart: source.Play);
    }

    public void PauseFade()
    {
        VolumeFade(1f, 0.5f);
        LowpassFade(1f, 3000f);
    }

    public void ResumeFade()
    {
        VolumeFade(1f, 1f);
        LowpassFade(1f, 22000f);
    }

    private void VolumeFade(float duration, float targetVolume, Action onStart = null, Action onComplete = null)
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

    public void LowpassFade(float duration, float targetValue)
    {
        StartCoroutine(_LowpassFade(duration, targetValue));
    }

    private IEnumerator _LowpassFade(float duration, float targetValue)
    {
        float timer = 0f;
        float startVolume = lowPass.cutoffFrequency;

        while (timer < duration)
        {
            lowPass.cutoffFrequency = Mathf.Lerp(startVolume, targetValue, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
