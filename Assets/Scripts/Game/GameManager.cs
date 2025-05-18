using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameOverMenu gameOverMenu;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private CamShutter camShutter;
    [SerializeField] private LevelTimer levelTimer;

    public bool gameOver { get; private set; } = false;

    private PlayerHealth playerHealth;

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

        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        if (LevelTimer.Instance != null)
            LevelTimer.Instance.StartTimer();

        camShutter.ShutterOpen();
    }

    public void WinGame()
    {
        GameOver(GameOverType.Win);
    }

    public void LoseGame()
    {
        GameOver(GameOverType.Lose);
    }

    private void GameOver(GameOverType gameOverType)
    {
        if (gameOver) return;
        gameOver = true;

        LevelTimer.Instance.StopTimer();
        MusicPlayer.Instance.PauseFade();
        camShutter.ShutterClose();

        if (gameOverType == GameOverType.Win)
            playerHealth.SetInvincible(true);

        StartCoroutine(GameOver_(gameOverType));
    }

    private IEnumerator GameOver_(GameOverType gameOverType)
    {
        yield return new WaitForSeconds(2f);

        switch (gameOverType)
        {
            case GameOverType.Win:
                float thisTime = LevelTimer.Instance.ElapsedTime;
                float bestTime = PlayerPrefs.GetFloat("bestTime");
                if (thisTime < bestTime || bestTime < 0f)
                {
                    bestTime = thisTime;
                    PlayerPrefs.SetFloat("bestTime", thisTime);
                }
                gameOverMenu.ShowWinScreen(LevelTimer.Instance.GetTimeString(), LevelTimer.Instance.FormatTimeToString(bestTime), LevelManager.Instance.enemiesKilled.ToString());
                break;

            case GameOverType.Lose:
                gameOverMenu.ShowLoseScreen(LevelTimer.Instance.GetTimeString());
                break;
        }
    }

    public void PauseGame()
    {
        pauseMenu.ShowMenu();
        LevelTimer.Instance.StopTimer();
        MusicPlayer.Instance.PauseFade();
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseMenu.HideMenu();
        LevelTimer.Instance.StartTimer();
        MusicPlayer.Instance.ResumeFade();
        Time.timeScale = 1f;
    }

    public void RestartLevel()
    {
        LoadingScreen.Instance.LoadScene(LoadingScreen.Instance.ActiveScene);
    }

    public void ToMainMenu()
    {
        LoadingScreen.Instance.LoadScene("Menu");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        PlayerPrefs.Save();
        Application.Quit();
    }
}

public enum GameOverType
{
    Lose,
    Win
}
