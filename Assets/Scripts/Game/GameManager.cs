using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool gameOver { get; private set; } = false;
    public GameOverType gameOverType { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void WinGame()
    {
        gameOver = true;
        gameOverType = GameOverType.Win;
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

        GameOverUI.Instance.ShowScreen(gameOverType);
    }
}

public enum GameOverType
{
    Lose = 0,
    Win = 1
}
