using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

public enum EGameState
{
    None = 0,
    MainMenu = 1,
    GamePlay = 2,
    Pause = 3,
    GameOver = 4,
    Win = 5,
    SelectLevel = 6,
}

public class GameManager : Singleton<GameManager>
{
    //[SerializeField] UserData userData;
    //[SerializeField] CSVData csv;
    private static EGameState _gameState = EGameState.MainMenu;

    protected void Awake()
    {
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }
        ChangeState(EGameState.MainMenu);
    }

    public void ChangeState(EGameState state)
    {
        _gameState = state;
        switch (_gameState)
        {
            case EGameState.None:
                break;
            case EGameState.MainMenu: 
                HandleGameStateMainMenu();
                break;
            case EGameState.GamePlay:
                HandleGameStateGamePlay();
                break;
            case EGameState.Pause:
                break;
            case EGameState.GameOver:
                break;
            case EGameState.Win:
                break;
            case EGameState.SelectLevel:
                HandleGameStateSelectLevel();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void HandleGameStateMainMenu()
    {
        UserDataManager.Ins.LoadData();
        AudioManager.Ins.Setup();
        UIManager.Ins.OpenUI<CanvasMainMenu>(ECanvasID.CanvasMainMenu);
    }

    private void HandleGameStateGamePlay()
    {
        UIManager.Ins.CloseUI(ECanvasID.CanvasMainMenu);
        UIManager.Ins.OpenUI<CanvasGamePlay>(ECanvasID.CanvasGamePlay);
        LevelManager.Ins.LoadGamePlay().Forget();
    }
    
    private void HandleGameStateSelectLevel()
    {
        UIManager.Ins.OpenUI<CanvasSelectLevel>(ECanvasID.CanvasSelectLevel);
    }
}
