using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
using TMPro;
using R3;

public class CanvasSelectLevel : UICanvas
{
    [SerializeField] private Button btnBack;
    [SerializeField] private TextMeshProUGUI tmpKeyAmount;
    [SerializeField] private TextMeshProUGUI tmpProgress;
    [SerializeField] private AssetReference assetRefLevelUIButtonSelectLevel;
    [SerializeField] private Transform tfContent;
    
    private void Start()
    {
        btnBack.onClick.AddListener(CloseCanvas);
        UserDataManager.Ins.UserData.KeyAmount.Subscribe(UpdateTextKeyAmount);
        UpdateTextKeyAmount(UserDataManager.Ins.UserData.KeyAmount.Value);
        UpdateProgress();
        LoadLevelButton().Forget();
    }

    private void UpdateProgress()
    {
        int passedLevels = UserDataManager.Ins.GetPassedLevels();
        int totalLevels = LevelDataConfigList.Instance.LevelDataConfigs.Count;
        tmpProgress.SetText($"{passedLevels}/{totalLevels}");
    }
    
    private void CloseCanvas()
    {
        Close();
        GameManager.Ins.ChangeState(EGameState.MainMenu);
    }
    
    private void UpdateTextKeyAmount(int amount)
    {
        tmpKeyAmount.SetText($"{amount}");
    }
    
    private async UniTask LoadLevelButton()
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(assetRefLevelUIButtonSelectLevel);
        await handle.ToUniTask();
        for (int i = 0; i < LevelDataConfigList.Instance.LevelDataConfigs.Count; i++)
        {
            LevelDataConfig levelDataConfig = LevelDataConfigList.Instance.LevelDataConfigs[i];
            LevelSaveData levelSaveData = UserDataManager.Ins.UserData.FindMatchLevelSaveData(levelDataConfig.StrID);
            UIButtonSelectLevel button = Instantiate(handle.Result, tfContent).GetComponent<UIButtonSelectLevel>();
            button.Setup(levelDataConfig, levelSaveData);
        }
        Addressables.Release(handle);
    }
}
