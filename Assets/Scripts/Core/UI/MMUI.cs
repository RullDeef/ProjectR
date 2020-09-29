using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    // MMUI stands for Main Menu UI (need renaming though...)
    public class MMUI : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("DemoGameScene");
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}