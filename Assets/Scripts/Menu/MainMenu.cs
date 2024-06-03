using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SaveData progressData = DataManager.LoadProgressData();

        string sceneName;

        if (progressData.lastPlayedLevel != null)
        {
            sceneName = progressData.lastPlayedLevel.levelSceneName;
        }
        else
        {
            sceneName = "Level_1";
        }

        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }
}
