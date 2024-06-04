using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameOverMenu gameOverMenu;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private LevelTimer levelTimer;

    public bool gameOver { get; private set; } = false;
    public GameOverType gameOverType { get; private set; }

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

    public void StartLevel(LevelDataSO levelData)
    {
        levelTimer.ResetTimer();
        levelTimer.StartTimer();
        MusicPlayer.Instance.StartPlaying();
    }

    public void WinGame()
    {
        gameOver = true;
        gameOverType = GameOverType.Win;
        Invoke("GameOver", 2f);
    }

    public void LoseGame()
    {
        gameOver = true;
        gameOverType = GameOverType.Lose;
        Invoke("GameOver", 0f);
    }

    private void GameOver()
    {
        StartCoroutine(GameOver_());
    }

    private IEnumerator GameOver_()
    {
        yield return new WaitForSeconds(2f);

        levelTimer.StopTimer();
        MusicPlayer.Instance.StopPlaying();

        switch (gameOverType)
        {
            case GameOverType.Win:
                gameOverMenu.ShowWinScreen();
                break;

            case GameOverType.Lose:
                gameOverMenu.ShowLoseScreen();
                break;
        }
    }

    public void PauseGame()
    {
        Debug.Log($"Paused Game");
        pauseMenu.ShowMenu();
        levelTimer.StopTimer();
        MusicPlayer.Instance.VolumeFade(1f, 0.25f);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Debug.Log($"Resumed Game");
        pauseMenu.HideMenu();
        levelTimer.StartTimer();
        MusicPlayer.Instance.VolumeFade(1f, 1f);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        // Save data before exiting
        // DataManager.SaveProgressData(!!!);

        Application.Quit();
    }
}

public enum GameOverType
{
    Lose = 0,
    Win = 1
}
