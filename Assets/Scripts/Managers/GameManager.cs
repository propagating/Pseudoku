using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    MAINMENU,
    INGAME,
    PAUSE,
    GAMEOVER
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private GameStates currentState;
    public static GameStates CurrentState  => instance.currentState;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public static void ChangeState(GameStates state)
    {
        if (instance.currentState == state)
            return;

        instance.currentState = state;

        switch (instance.currentState)
        {
            case GameStates.MAINMENU:
                Time.timeScale = 1;
                break;
            case GameStates.INGAME:
                Time.timeScale = 1;
                break;
            case GameStates.PAUSE:
                Time.timeScale = 0;
                break;
            case GameStates.GAMEOVER:
                Time.timeScale = 0;
                break;
        }
    }
}
