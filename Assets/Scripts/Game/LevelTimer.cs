using System;
using TMPro;
using UnityEditor;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public static LevelTimer Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI timerText;

    public TimeSpan elapsedTime { get; private set; } = TimeSpan.Zero;

    private float _elapsedTime = 0f;
    public float ElapsedTime => _elapsedTime;
    private bool running = false;

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
        return FormatTimeToString(_elapsedTime);
    }

    public string FormatTimeToString(float timeFloat)
    {
        if (timeFloat < 0f) return "N/A";

        TimeSpan timeSpan = TimeSpan.FromSeconds(timeFloat);
        return string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
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

#if UNITY_EDITOR

    public void ResetBestTime()
    {
        PlayerPrefs.SetFloat("bestTime", -1f);
    }

    public string GetBestTime()
    {
        return FormatTimeToString(PlayerPrefs.GetFloat("bestTime"));
    }

#endif

}

#if UNITY_EDITOR

[CustomEditor(typeof(LevelTimer))]
public class LevelTimerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Reset Best Time"))
        {
            var script = (LevelTimer)target;
            script.ResetBestTime();
        }
        if (GUILayout.Button("Get Best Time"))
        {
            var script = (LevelTimer)target;
            Debug.Log(script.GetBestTime());
        }
        EditorGUILayout.EndHorizontal();
    }
}

#endif
