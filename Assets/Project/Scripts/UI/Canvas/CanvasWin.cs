using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasWin : UICanvas
{
    [SerializeField] private Button btnNextLevel;
    [SerializeField] private Button btnWactchAdsEarnKey;
    
    private void Start()
    {
        btnNextLevel.onClick.AddListener(OnClickNextLevel);
        btnWactchAdsEarnKey.onClick.AddListener(OnClickWatchAdsEarnKey);
    }

    private void OnClickWatchAdsEarnKey()
    {
        UserDataManager.Ins.EarnKey(1);
        Close();
    }

    private void OnClickNextLevel()
    {
        LevelManager.Ins.NextLevel();
        Close();
    }
}
