using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public enum GamingState
    {
        RealTime,
        Fight
    }

    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public RTUI RTUI;
        public FightUI fightUI;

        public GamingState gamingState;
        private bool isPaused = false;

        private void Awake()
        {
            instance = this;
            RTUI = FindObjectOfType<RTUI>();
            fightUI = FindObjectOfType<FightUI>();

            if (RTUI != null)
                gamingState = GamingState.RealTime;
            else
                gamingState = GamingState.Fight;
        }

        public static bool IsPaused()
        {
            return instance.isPaused;
        }

        public static void PauseGame()
        {
            instance.isPaused = true;

            if (instance.gamingState == GamingState.RealTime)
                instance.RTUI.OpenPauseMenu();
            else
                instance.fightUI.OpenPauseMenu();
        }

        public static void ResumeGame()
        {
            if (instance.gamingState == GamingState.RealTime)
                instance.RTUI.ClosePauseMenu();
            else
                instance.fightUI.ClosePauseMenu();
            
            instance.isPaused = false;
        }

        public static void GoToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}