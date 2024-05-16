using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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

        LevelTimer.Instance.StopTimer();
        GameOverMenu.Instance.ShowScreen(gameOverType);
    }

    public void PauseGame()
    {
        LevelTimer.Instance.StopTimer();
        PauseMenu.Instance.SetMenuActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        PauseMenu.Instance.SetMenuActive(false);
        LevelTimer.Instance.StartTimer();
        Time.timeScale = 1f;
    }
}

public enum GameOverType
{
    Lose = 0,
    Win = 1
}
