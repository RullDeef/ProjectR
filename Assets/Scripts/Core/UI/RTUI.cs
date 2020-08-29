using UnityEngine;

namespace Core
{
    public class RTUI : MonoBehaviour
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

        public void GoToMainMenu()
        {
            GameManager.GoToMainMenu();
        }
    }
}