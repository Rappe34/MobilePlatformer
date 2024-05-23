using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagement
{
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
}
