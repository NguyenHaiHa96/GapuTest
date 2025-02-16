using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using R3;

public class CanvasMainMenu : UICanvas
{
    [SerializeField] private TextMeshProUGUI tmpKeyAmount;
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnLevelSelect;
    
    private void Start()
    {
        UserDataManager.Ins.UserData.KeyAmount.Subscribe(UpdateTextKeyAmount);
        UpdateTextKeyAmount(UserDataManager.Ins.UserData.KeyAmount.Value);
        btnPlay.onClick.AddListener(OnClickPlay);
        btnLevelSelect.onClick.AddListener(OnClickSelectLevel);
    }

    private void OnClickSelectLevel()
    {
        Close();
        GameManager.Ins.ChangeState(EGameState.SelectLevel);
    }

    private void OnClickPlay()
    {
        GameManager.Ins.ChangeState(EGameState.GamePlay);
    }

    private void UpdateTextKeyAmount(int amount)
    {
        tmpKeyAmount.SetText($"{amount}");
    }
}