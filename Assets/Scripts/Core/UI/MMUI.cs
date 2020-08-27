using UnityEngine;
using UnityEngine.SceneManagement;

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
