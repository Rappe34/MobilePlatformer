using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        losePanel.SetActive(false);
        winPanel.SetActive(false);
    }

    public void ShowScreen(GameManager.GameOverType gameOverType)
    {
        switch (gameOverType)
        {
            case GameManager.GameOverType.Lose:
                losePanel.SetActive(true);
                break;

            case GameManager.GameOverType.Win:
                winPanel.SetActive(true);
                break;
        }
    }
}
