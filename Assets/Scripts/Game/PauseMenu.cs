using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagement
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenuPanel;
        [SerializeField] private GameObject settingsMenuPanel;
        [SerializeField] private GameObject blurVolume;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            pauseMenuPanel.SetActive(false);
            settingsMenuPanel.SetActive(false);
            blurVolume.SetActive(false);
        }

        public void ToggleMenu()
        {
            pauseMenuPanel.SetActive(!pauseMenuPanel.activeInHierarchy);
            settingsMenuPanel.SetActive(false);
            blurVolume.SetActive(!blurVolume.activeInHierarchy);
        }

        public void ToggleSettingsMenu()
        {
            pauseMenuPanel.SetActive(!pauseMenuPanel.activeInHierarchy);
            settingsMenuPanel.SetActive(!settingsMenuPanel.activeInHierarchy);
        }
    }
}
