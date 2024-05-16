using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public static GameOverMenu Instance { get; private set; }

    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        losePanel.SetActive(false);
        winPanel.SetActive(false);
    }

    public void ShowScreen(GameOverType gameOverType)
    {
        switch (gameOverType)
        {
            case GameOverType.Lose:
                losePanel.SetActive(true);
                break;

            case GameOverType.Win:
                winPanel.SetActive(true);
                break;
        }
    }
}
