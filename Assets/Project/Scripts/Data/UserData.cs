using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using R3;
using UnityEngine.Serialization;

[Serializable]
public class UserData
{
    [Title("Data", "Resources")]
    [SerializeField] private SerializableReactiveProperty<int> keyAmount;
    [SerializeField] private SerializableReactiveProperty<int> hintAmount;
    
    [Title("", "Progress")]
    [SerializeField] private List<LevelSaveData> levelSaveDataList;
    
    [Title("", "Settings")]
    [SerializeField] private SerializableReactiveProperty<bool> soundActive;
    [SerializeField] private SerializableReactiveProperty<bool> musicActive;
    
    [SerializeField] private SerializableReactiveProperty<string> strLastLevel = new();
    
    public ReactiveProperty<string> StrLastLevel => strLastLevel;
    public ReactiveProperty<int> KeyAmount => keyAmount;
    public ReactiveProperty<int> HintAmount => hintAmount;
    public ReactiveProperty<bool> SoundActive => soundActive;
    public ReactiveProperty<bool> MusicActive => musicActive;
    public List<LevelSaveData> LevelSaveDataList => levelSaveDataList;
    
    public UserData()
    {
        keyAmount = new SerializableReactiveProperty<int>();
        hintAmount = new SerializableReactiveProperty<int>();
        soundActive = new SerializableReactiveProperty<bool>(true);
        musicActive = new SerializableReactiveProperty<bool>(true);
        strLastLevel.Value = Constants.STR_STARTING_LEVEL;
        levelSaveDataList = new List<LevelSaveData>();
        AddNewLevelSaveData(strLastLevel.Value);
    }

    public void AddNewLevelSaveData(string levelID)
    {
        LevelSaveData levelSaveData = LevelSaveDataList.Find(x => x.StringID == strLastLevel.Value);
        if (levelSaveDataList.Contains(levelSaveData)) return;
        levelSaveDataList.Add(new LevelSaveData(levelID));
    }

    public LevelSaveData FindMatchLevelSaveData(string levelID)
    {
        return LevelSaveDataList.Find(x => x.StringID == levelID);
    }
}

[Serializable] 
public class LevelSaveData
{
    [SerializeField] private string stringID;
    [SerializeField] private bool isUnlocked;
    [SerializeField] private bool isPassed;

    public LevelSaveData(string stringID)
    {
        this.stringID = stringID;
        isUnlocked = true;
        isPassed = false;
    }
    
    public string StringID { get => stringID; set => stringID = value; }
    public bool IsUnlocked { get => isUnlocked; set => isUnlocked = value; }
    public bool IsPassed { get => isPassed; set => isPassed = value; }
}
