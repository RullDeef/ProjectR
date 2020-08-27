using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public RTUI RTUI;
    public bool isPaused { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
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
