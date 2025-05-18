using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private WinPanel winPanel;
    [SerializeField] private LosePanel losePanel;
    [SerializeField] private GameObject confirmToMenuPanel;
    [SerializeField] private GameObject confirmExitPanel;
    [SerializeField] private GameObject blurVolume;

    private void Start()
    {
        losePanel.gameObject.SetActive(false);
        winPanel.gameObject.SetActive(false);
        confirmToMenuPanel.SetActive(false);
        confirmExitPanel.SetActive(false);
        blurVolume.SetActive(false);
    }

    public void ShowWinScreen(string timeString, string bestTimeString, string killsString)
    {
        winPanel.ShowPanel(timeString, bestTimeString, killsString);
        blurVolume.SetActive(true);
    }

    public void ShowLoseScreen(string timeString)
    {
        losePanel.ShowPanel(timeString);
        blurVolume.SetActive(true);
    }

    public void HideMenu()
    {
        losePanel.gameObject.SetActive(false);
        winPanel.gameObject.SetActive(false);
        confirmToMenuPanel.SetActive(false);
        confirmExitPanel.SetActive(false);
        blurVolume.SetActive(false);
    }
}
