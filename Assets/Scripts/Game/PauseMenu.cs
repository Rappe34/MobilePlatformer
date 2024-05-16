using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance { get; private set; }

    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject blurVolume;

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

    private void Start()
    {
        blurVolume.SetActive(false);
    }

    public void SetMenuActive(bool paused)
    {
        pauseMenuPanel.SetActive(paused);
        blurVolume.SetActive(paused);
    }
}
