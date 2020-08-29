using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
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