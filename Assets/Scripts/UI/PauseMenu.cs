using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject settingsMenuPanel;
    [SerializeField] private GameObject confirmToMenuPanel;
    [SerializeField] private GameObject confirmExitPanel;
    [SerializeField] private GameObject blurVolume;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        pauseMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(false);
        confirmToMenuPanel.SetActive(false);
        confirmExitPanel.SetActive(false);
        blurVolume.SetActive(false);
    }

    public void ShowMenu()
    {
        pauseMenuPanel.SetActive(true);
        blurVolume.SetActive(true);
    }

    public void HideMenu()
    {
        pauseMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(false);
        confirmToMenuPanel.SetActive(false);
        confirmExitPanel.SetActive(false);
        blurVolume.SetActive(false);
    }
}
