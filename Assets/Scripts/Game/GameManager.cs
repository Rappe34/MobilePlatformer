using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void SwitchScene(string sceneName)
    {

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
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Debug.Log($"Resumed Game - {Time.time}");
        pauseMenu.HideMenu();
        levelTimer.StartTimer();
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

public enum GameOverType
{
    Lose = 0,
    Win = 1
}
