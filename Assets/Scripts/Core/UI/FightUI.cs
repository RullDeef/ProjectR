using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class FightUI : MonoBehaviour
    {
        public GameObject pauseMenu;

        public void OpenPauseMenu()
        {
            pauseMenu.SetActive(true);
        }

        public void ClosePauseMenu()
        {
            pauseMenu.SetActive(false);
        }

        public void PauseGame()
        {
            GameManager.PauseGame();
        }

        public void ResumeGame()
        {
            GameManager.ResumeGame();
        }
    }
}