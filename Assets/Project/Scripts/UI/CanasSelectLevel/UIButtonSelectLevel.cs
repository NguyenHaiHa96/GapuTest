using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ELevelState
{
    None = 0,
    Passed = 1,
    Unlocked = 2,
    Lock = 3,
}

public class UIButtonSelectLevel : MonoBehaviour
{
    [SerializeField] private ELevelState status;
    [SerializeField] private Button btnSelectLevel;
    [SerializeField] private Image imgBg;
    [SerializeField] private Image imgPassed;
    [SerializeField] private Image imgLock;
    [SerializeField] private Image imgNew;
    [SerializeField] private TextMeshProUGUI tmpLevel;

    [BoxGroup("Background Color")] [SerializeField] private Color colorBgPassed;
    [BoxGroup("Background Color")] [SerializeField] private Color colorBgUnlocked;
    [BoxGroup("Background Color")] [SerializeField] private Color colorBgLock;
    
    private string _levelID;
    
    private void Start()
    {
        btnSelectLevel.onClick.AddListener(OnClickSelectLevel);
    }

    private void OnClickSelectLevel()
    {
        if (LevelManager.Ins.IsLevelUnlocked(_levelID))
        {
            UIManager.Ins.CloseUI(ECanvasID.CanvasSelectLevel);
            GameManager.Ins.ChangeState(EGameState.GamePlay);
        }
        else
        {
            // TODO: Show pop up
        }
    }

    public void Setup(LevelDataConfig levelDataConfig, LevelSaveData levelSaveData)
    {
        tmpLevel.SetText($"{levelDataConfig.StrID.Replace(Constants.STR_LEVEL, string.Empty)}");
        _levelID = levelDataConfig.StrID;
        if (levelSaveData == null)
        {
            status = ELevelState.Lock;
        }
        else
        {
            status = levelSaveData.IsPassed ? ELevelState.Passed : ELevelState.Unlocked;
        }

        HandleLevelStatus();
    }

    private void HandleLevelStatus()
    {
        switch (status)
        {
            case ELevelState.None:
                break;
            case ELevelState.Lock:
                HandleStatusLock();
                break;
            case ELevelState.Passed:
                HandleStatusPassed();
                break;
            case ELevelState.Unlocked:
                HandleStatusUnlocked();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void HandleStatusUnlocked()
    {
        imgBg.color = colorBgUnlocked;
        imgPassed.gameObject.SetActive(false);
        imgLock.gameObject.SetActive(false);
        imgNew.gameObject.SetActive(true);
    }

    private void HandleStatusPassed()
    {
        imgBg.color = colorBgPassed;
        imgPassed.gameObject.SetActive(true);
        imgLock.gameObject.SetActive(false);
        imgNew.gameObject.SetActive(false);
    }

    private void HandleStatusLock()
    {
        imgBg.color = colorBgLock;
        imgPassed.gameObject.SetActive(false);
        imgLock.gameObject.SetActive(true);
        imgNew.gameObject.SetActive(false);
    }
}
