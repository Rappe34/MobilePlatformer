using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class DataManager
{
    private static readonly string SaveFilePath = Path.Combine(Application.persistentDataPath, "playerProgress.json");

    public static void SaveProgressData(SaveData progress)
    {
        try
        {
            string json = JsonUtility.ToJson(progress, true);
            File.WriteAllText(SaveFilePath, json);
            Debug.Log("Progress saved successfully.");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save progress: {e.Message}");
        }
    }

    public static SaveData LoadProgressData()
    {
        if (!File.Exists(SaveFilePath))
        {
            Debug.LogWarning("Save file not found, returning new progress.");
            return new SaveData
            {
                lastPlayedLevel = (1, "Level_1"),
                bossesDefeated = new List<int>(),
                fastestLevelTimes = new Dictionary<int, float>()
            };
        }

        try
        {
            string json = File.ReadAllText(SaveFilePath);
            SaveData progress = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("Progress loaded successfully.");
            return progress;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load progress: {e.Message}");
            return new SaveData
            {
                lastPlayedLevel = (1, "Level_1"),
                bossesDefeated = new List<int>(),
                fastestLevelTimes = new Dictionary<int, float>()
            };
        }
    }
}
