using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagement
{
    public class GameUIActions : MonoBehaviour
    {
        public void PressPauseButton()
        {
            GameManager.Instance.PauseGame();
        }
    }

}
