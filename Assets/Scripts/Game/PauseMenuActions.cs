using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManagement;

namespace GameManagement
{
    public class PauseMenuActions : MonoBehaviour
    {
        public void PressResume()
        {
            GameManager.Instance.ResumeGame();
        }

        public void PressSettings()
        {
            GameManager.Instance.TogglePauseSettingsMenu();
        }

        public void PressMainMenu()
        {
            GameManager.Instance.SwitchScene("Menu");
        }

        public void PressExit()
        {


#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
