using System.Linq;
using UnityEngine;

public class UserDataManager : Singleton<UserDataManager>
{
    [SerializeField] private UserData userData;
    
    public UserData UserData => userData;
    
    public void LoadData()
    {
        string saveData = PlayerPrefs.GetString(Constants.STR_KEY_USER_DATA, 
            string.Empty);
        if (!string.IsNullOrEmpty(saveData))
        {
            userData = JsonUtility.FromJson<UserData>(saveData);
        }
        else
        {
            userData = new UserData();
            SaveData();
        }
    }
    
    public void SaveData()
    {
        PlayerPrefs.SetString(Constants.STR_KEY_USER_DATA, 
            JsonUtility.ToJson(userData));
    }

    public void SetLevelPassed()
    {
        LevelSaveData levelSaveData = userData.FindMatchLevelSaveData(userData.StrLastLevel.Value);
        if (levelSaveData == null) return;
        if (levelSaveData.IsPassed) return;
        levelSaveData.IsPassed = true;
    }

    public void SetLastLevel(string nextLevelStringID)
    {
        userData.StrLastLevel.Value = nextLevelStringID;
        SaveData();
    }
    
    public void EarnKey(int amount)
    {
        userData.KeyAmount.Value += amount;
        SaveData();
    }

    public void AddNewLevelSaveData(string nextLevelStringID)
    {
        userData.AddNewLevelSaveData(nextLevelStringID);
        Debug.Log("Add new level save data: " + nextLevelStringID);
        SaveData();
    }

    public bool IsLevelUnlocked(string levelID)
    {
        LevelSaveData levelSaveData = userData.FindMatchLevelSaveData(levelID);
        return levelSaveData is { IsUnlocked: true };
    }

    public int GetPassedLevels()
    {
        return userData.LevelSaveDataList.Count(level => level.IsPassed);
    }

    public bool HaveAvailableKey()
    {
        return userData.HintAmount.Value > 0;
    }

    public void ConsumeKey(int amount)
    {
        userData.KeyAmount.Value -= amount;
        SaveData();
    }
}
