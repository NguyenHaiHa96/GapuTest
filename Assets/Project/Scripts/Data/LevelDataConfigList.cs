using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;

[CreateAssetMenu(fileName = "LevelDataConfigList", menuName = "Scriptable Objects/Level Data Config List")]
public class LevelDataConfigList : GlobalConfig<LevelDataConfigList>
{
    #region Editor

#if UNITY_EDITOR
    
    private const string STR_HINT_PREFIX = "hint-";
    
    [Title("Data Config", "Sheet")]
    [BoxGroup("Sheet")]
    [SerializeField] private string linkSheet;
    [BoxGroup("Sheet")]
    [SerializeField] private string tabLevelConfigData;
    [BoxGroup("Sheet")]
    [SerializeField] private string folderHintSpritePath;
    
    [BoxGroup("Sheet")] [Button]
    private async void LoadLevelConfigDataFromSheet()
    {
        if (string.IsNullOrEmpty(linkSheet)) return;
        EditorUtility.SetDirty(this);
        levelDataConfigs.Clear();
        var data = await BakingSheet.GetCsv(linkSheet, tabLevelConfigData);
        List<Dictionary<string, string>> dataDictionaries = CSVReader.ReadStringData(data);
        for (int i = 0; i < dataDictionaries.Count; i++)
        {
            Dictionary<string, string> dataLine = dataDictionaries[i];
            LevelDataConfig levelDataConfig = new LevelDataConfig(dataLine);
            levelDataConfigs.Add(levelDataConfig);
        }
        AssetDatabase.SaveAssets();
    }
    
    [BoxGroup("Sheet")] [Button]
    private void LoadHintSpriteFromFolder()
    {
        string[] filePaths = System.IO.Directory.GetFiles(folderHintSpritePath);
        if (filePaths.Length <= 0) return;
        EditorUtility.SetDirty(this);
        List<Sprite> sprites = new();
        for (int i = 0; i < levelDataConfigs.Count; i++)
        {
            LevelDataConfig levelDataConfig = levelDataConfigs[i];
            levelDataConfig.SprHint = null;
        }
        for (int i = 0; i < filePaths.Length; i++)
        {
            UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(filePaths[i], 
                typeof(Sprite)) as Sprite;
            if (obj is not Sprite asset) continue;
            sprites.Add(asset);
        }

        for (int i = 0; i < sprites.Count; i++)
        {
            string fileName = sprites[i].name;
            Sprite sprite = sprites[i];
            string temp = fileName.Replace(STR_HINT_PREFIX, string.Empty);
            temp = char.ToUpper(temp[0]) + temp[1..];
            LevelDataConfig levelDataConfig = levelDataConfigs
                .Find(x => x.StrID.Equals(temp));
            if (levelDataConfig == null) continue;
            levelDataConfig.SprHint = sprite;
        }
    }
    
#endif
    
#endregion

    [Title("", "Data")]
    [SerializeField] private List<LevelDataConfig> levelDataConfigs = new();
    
    public List<LevelDataConfig> LevelDataConfigs => levelDataConfigs;
    
    public LevelDataConfig GetLevelDataConfig(string keyID)
    {
        return levelDataConfigs.Find(x => x.StrID.Equals(keyID));
    }
}

[Serializable]
public class LevelDataConfig
{
    #region Editor

    private const string STR_ID = "ID";
    private const string STR_TIME = "Time";
    private const string STR_DESCRIPTION = "Description";   
    private const string STR_END_GAME_DESCRIPTION = "End Game Description";
    
#if UNITY_EDITOR

    public LevelDataConfig(Dictionary<string, string> dataLine)
    {
        strID = string.Format($"{Constants.STR_LEVEL}{dataLine[STR_ID]}");
        time = float.Parse(dataLine[STR_TIME]);
        strDescription = dataLine[STR_DESCRIPTION];
        strEndGameDescription = dataLine[STR_END_GAME_DESCRIPTION];
    }
    
#endif

    #endregion
    
    [SerializeField] private string strID;
    [PreviewField(80, ObjectFieldAlignment.Center)] 
    [SerializeField] private Sprite sprHint;
    [SerializeField] private float time;
    [SerializeField] private string strDescription;
    [SerializeField] private string strEndGameDescription;
    
    public string StrID => strID;
    public Sprite SprHint { get => sprHint; set => sprHint = value; }
    public float Time => time;
    public string StrDescription => strDescription;
    public string StrEndGameDescription => strEndGameDescription;
}

