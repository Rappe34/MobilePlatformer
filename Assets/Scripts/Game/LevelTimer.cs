using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    public static LevelTimer Instance;

    private float startTime;
    private bool running = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        startTime = Time.time;
        running = true;
    }

    private void Update()
    {
        if (running)
            UpdateTimer();
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

    public void StopTimer()
    {
        running = false;
    }

    public void ResumeTimer()
    {
        startTime = Time.time - (float.Parse(timerText.text.Split(':')[0]) * 3600f) - (float.Parse(timerText.text.Split(':')[1]) * 60f) - float.Parse(timerText.text.Split(':')[2]);
        running = true;
    }
}
