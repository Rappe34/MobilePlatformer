using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameOverMenu gameOverMenu;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private GameLoadingPanel gameLoadingPanel;
    [SerializeField] private LevelTimer levelTimer;
    [SerializeField] private MusicPlayer musicPlayer;

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

    public void LoadLevel(LevelDataSO levelData)
    {
        StartCoroutine(LoadLevel_(levelData));
    }

    private IEnumerator LoadLevel_(LevelDataSO levelData)
    {
        yield return StartCoroutine(SceneLoader.Instance.LoadScene(levelData.levelSceneName));

        // Activate the scene once loading is complete
        SceneLoader.Instance.ActivateScene();
        levelTimer.StartTimer();
        musicPlayer.PlayTrack(levelData.levelSong);
    }

    public void WinGame()
    {
        gameOver = true;
        gameOverType = GameOverType.Win;
        StartCoroutine(GameOver());
    }

    public void LoseGame()
    {
        gameOver = true;
        gameOverType = GameOverType.Lose;
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);

        levelTimer.StopTimer();

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
        Debug.Log($"Paused Game - {Time.time}");
        pauseMenu.ShowMenu();
        levelTimer.StopTimer();
        musicPlayer.VolumeFade(1f, 0.25f);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Debug.Log($"Resumed Game - {Time.time}");
        pauseMenu.HideMenu();
        levelTimer.StartTimer();
        musicPlayer.VolumeFade(1f, 1f);
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
