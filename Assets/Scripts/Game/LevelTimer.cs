using System;
using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public static LevelTimer Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI timerText;

    public TimeSpan elapsedTime { get; private set; } = TimeSpan.Zero;

    private float _elapsedTime = 0f;
    private bool running = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (running) UpdateTimer();
    }

    private void UpdateTimer()
    {
        _elapsedTime += Time.deltaTime;
        elapsedTime = TimeSpan.FromSeconds(_elapsedTime);
        timerText.text = GetTimeString();
    }

    public string GetTimeString()
    {
        return string.Format("{0:D2}:{1:D2}:{2:D2}", elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds);
    }

    public void StartTimer()
    {
        running = true;
    }

    public void StopTimer()
    {
        running = false;
    }

    public void ResetTimer()
    {
        _elapsedTime = 0f;
    }
}
