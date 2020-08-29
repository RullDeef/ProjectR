﻿using UnityEngine;

namespace Core
{
    public class RTUI : MonoBehaviour
    {
        public GameObject pauseMenu;
        public GameObject inventoryMenu;

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

        public void GoToMainMenu()
        {
            GameManager.GoToMainMenu();
        }

        public bool IsInventoryOpen()
        {
            return inventoryMenu.activeSelf;
        }

        public void OpenInventory()
        {
            inventoryMenu.SetActive(true);
        }

        public void CloseInventory()
        {
            inventoryMenu.SetActive(false);
        }
    }
}