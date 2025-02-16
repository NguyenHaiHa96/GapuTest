using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;

public class LevelManager : Singleton<LevelManager>
{
    [Title("Level Manager", "Game Play")]
    [SerializeField] private Camera mainCam;
    [SerializeField] private AssetReference assetReferenceDrawingLine;
    [SerializeField] private DrawingLine drawingLine;
    
    [Title("", "Level")]
    [SerializeField] private string strLevelPrefabPath;
    [SerializeField] private LevelBase currentLevel;
    
    public UnityEvent onAfterDrawingCallback;
    public UnityEvent onWinCallback;
    public UnityEvent onLoseCallback;
    public UnityEvent onNextLevelCallback;
    public LevelBase CurrentLevel => currentLevel;
    
    private string _strLevelPath;
    private bool _isAddedListener;
    
    #region Editor
    
#if UNITY_EDITOR
    
    [Title("", "Editor")]
    [SerializeField]  private string levelTestID;

    [Button]
    private void TestLevel()
    {
        LoadLevel(levelTestID);
    }
    
#endif

    #endregion
    
    public Camera MainCam => mainCam;
    
    private void Awake()
    {
        OnInit();
    }
    
    private void OnInit()
    {
        mainCam = Camera.main;
    }
    
    #region Load

    public async UniTask LoadGamePlay()
    {
        await LoadDrawingLine();
        await LoadLevelByStringKeyID(UserDataManager.Ins.UserData.StrLastLevel.Value);
    }
    
    private async UniTask LoadDrawingLine()
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(assetReferenceDrawingLine);
        await handle.ToUniTask();
        drawingLine = Instantiate(handle.Result).GetComponent<DrawingLine>();
        drawingLine.SetParent(transform);
    }
    
    private async UniTask LoadLevelByStringKeyID(string keyID)
    {
        Debug.Log($"Loading level: {keyID}...");
        _strLevelPath = string.Format($"{strLevelPrefabPath}/{keyID}.prefab");
        var handle = Addressables.LoadAssetAsync<GameObject>(_strLevelPath);
        await handle.ToUniTask();
        DeleteCurrentLevel();
        currentLevel = Instantiate(handle.Result).GetComponent<LevelBase>();
        currentLevel.OnLoadDone(this);
        currentLevel.SetLevelDataConfig(LevelDataConfigList.Instance.GetLevelDataConfig(keyID));
        AudioManager.Ins.LoadAllAudioSourcesInScene();
        Addressables.Release(handle);
        if (!_isAddedListener)
        {
            onLoseCallback.AddListener(OnLoseHandler);
            onAfterDrawingCallback.AddListener(OnAfterDrawingHandler); 
            _isAddedListener = true;
        }
    }
    
    private void OnAfterDrawingHandler()
    {
        currentLevel.OnAfterDrawing();
    }
    
    private void OnLoseHandler()
    {
        currentLevel.TriggerLose();
    }
    
    #endregion

    public Sprite GetCurrentLevelHint() => currentLevel.GetHint();

    public void OnAfterDrawing()
    {
        onAfterDrawingCallback?.Invoke();
    }

    public void TriggerLose()
    {
        drawingLine.ClearDrawing();
        onLoseCallback?.Invoke();
    }

    public void SetLevelPassed()
    {
        Debug.Log("Level Passed");
        UserDataManager.Ins.SetLevelPassed();
        string strLevelID = UserDataManager.Ins.UserData.StrLastLevel.Value;
        int currentLevelID = int.Parse(strLevelID.Replace(Constants.STR_LEVEL, string.Empty));
        int nextLevelID = currentLevelID + 1;
        string nextLevelStringID = string.Format($"{Constants.STR_LEVEL}{nextLevelID}");
        UserDataManager.Ins.SetLastLevel(nextLevelStringID);
        UserDataManager.Ins.AddNewLevelSaveData(nextLevelStringID);
    }

    public void NextLevel()
    {
        UIManager.Ins.GetUI<CanvasGamePlay>(ECanvasID.CanvasGamePlay).SetupCanvas();
        LoadLevel(UserDataManager.Ins.UserData.StrLastLevel.Value);
    }

    public void LoadLevel(string levelID)
    {
        drawingLine.ClearDrawing();
        LoadLevelByStringKeyID(levelID).Forget();
    }

    public bool IsLevelUnlocked(string levelID)
    {
        return UserDataManager.Ins.IsLevelUnlocked(levelID);
    }

    public float GetTimeCountdown()
    {
        return currentLevel.LevelDataConfig.Time;
    }

    public void DeleteCurrentLevel()
    {
        if (currentLevel) Destroy(currentLevel.gameObject);
    }
}
