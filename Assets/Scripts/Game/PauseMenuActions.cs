using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void PressExit()
    {

    }
}
