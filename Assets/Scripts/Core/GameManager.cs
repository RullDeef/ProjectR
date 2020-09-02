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

        [SerializeField]
        private Transform player = null;

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

        private void Update()
        {
            // check for inventory key pressed
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (gamingState == GamingState.RealTime)
                {
                    if (RTUI.IsInventoryOpen())
                    {
                        CloseInventory();
                    }
                    else
                    {
                        OpenInventory();
                    }
                }
            }
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

        public static Transform GetPlayer()
        {
            return instance.player;
        }

        public void OpenInventory()
        {
            player.GetComponent<RT.Player.RTPlayerController>().DeactivateControlls();
            RTUI.OpenInventory();
        }

        public void CloseInventory()
        {
            RTUI.CloseInventory();
            player.GetComponent<RT.Player.RTPlayerController>().ActivateControlls();
        }
    }
}