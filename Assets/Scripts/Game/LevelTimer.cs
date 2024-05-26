using System;
using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    public TimeSpan elapsedTimeInSeconds { get; private set; } = TimeSpan.Zero;

    private float elapsedTime;
    private float startTime;
    private bool running = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (running) UpdateTimer();
    }

    private void UpdateTimer()
    {
        // Calculate elapsed time
        float elapsedTime = Time.time - startTime;

        // Update the UI Text component with the current game timer value
        if (timerText != null)
        {
            timerText.text = FormatTime(elapsedTime);
        }
    }

    private string FormatTime(float timeInSeconds)
    {
        // Convert time in seconds to minutes and seconds
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSeconds);
        return string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
    }

    public void StartTimer()
    {
        startTime = Time.time;
        running = true;
    }

    public void StopTimer()
    {
        running = false;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
    }
}
