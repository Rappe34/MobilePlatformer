using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private WinPanel winPanel;
    [SerializeField] private LosePanel losePanel;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        losePanel.gameObject.SetActive(false);
        winPanel.gameObject.SetActive(false);
    }

    public void ShowWinScreen()
    {
        winPanel.ShowPanel();
    }

    public void ShowLoseScreen()
    {
        losePanel.ShowPanel();
    }
}
