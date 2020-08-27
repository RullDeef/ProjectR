using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public RTUI RTUI;
    private bool isPaused = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public static bool IsPaused()
    {
        return instance.isPaused;
    }

    public static void PauseGame()
    {
        instance.isPaused = true;
        instance.RTUI.OpenPauseMenu();
    }

    public static void ResumeGame()
    {
        instance.RTUI.ClosePauseMenu();
        instance.isPaused = false;
    }
}
