using System;
using System.Collections.Generic;
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

        public GamingState gamingState = GamingState.RealTime;

        [SerializeField]
        private Transform player = null;

        private bool isPaused = false;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if (instance.RTUI == null)
                    instance.RTUI = FindObjectOfType<RTUI>();
                if (instance.fightUI == null)
                    instance.fightUI = FindObjectOfType<FightUI>();

                Destroy(gameObject);
                return;
            }
        }

        private void Update()
        {
            if (gamingState == GamingState.RealTime)
            {
                // check for inventory key pressed
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    if (RTUI.IsInventoryOpen())
                        CloseInventory();
                    else
                        OpenInventory();
                }

                // check for craft key pressed
                if (Input.GetKeyDown(KeyCode.I))
                {
                    if (RTUI.IsCraftOpen())
                        CloseCraft();
                    else
                        OpenCraft();
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

        internal static void SetPlayer(Transform player)
        {
            instance.player = player;
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

        public void OpenCraft()
        {
            //player.GetComponent<RT.Player.RTPlayerController>().DeactivateControlls();
            RTUI.OpenCraft();
        }

        public void CloseCraft()
        {
            RTUI.CloseCraft();
            //player.GetComponent<RT.Player.RTPlayerController>().ActivateControlls();
        }

        /**
         *  @brief Инициализирует боевку игрока с врагами, переданными в списке аргументов.
         *
         *  Открывает сцену боевки, создает необходимые контроллеры и
         *  инициализирует очередь ходов.
         */
        public static void InitFight(List<Common.UnitStats> units)
        {
            instance.gamingState = GamingState.Fight;
            SceneManager.LoadScene("FightScene");
            FightManager.InitFight(units);
        }
    }
}