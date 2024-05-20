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
        gameOverMenu.ShowScreen(gameOverType);
    }

    public void PauseGame()
    {
        levelTimer.StopTimer();
        pauseMenu.ToggleMenu();
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseMenu.ToggleMenu();
        levelTimer.StartTimer();
        Time.timeScale = 1f;
    }

    public void TogglePauseSettingsMenu()
    {
        pauseMenu.ToggleSettingsMenu();
    }

    public enum GameOverType
    {
        Lose = 0,
        Win = 1
    }
}
