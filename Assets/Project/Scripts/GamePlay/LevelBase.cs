using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Serialization;
using Cysharp.Threading.Tasks;

public class LevelBase : MonoBehaviour
{
    [SerializeField] protected LevelDataConfig levelDataConfig;
    
    protected CancellationTokenSource cancellationTokenSource;
    
    public LevelDataConfig LevelDataConfig => levelDataConfig;
    
    public void SetLevelDataConfig(LevelDataConfig dataConfig)
    {
        levelDataConfig = dataConfig;
    }
    
    public virtual void OnStartLevel()
    {
        
    }
    public virtual void OnAfterDrawing()
    {
        
    }
    
    public virtual void OnEndLevel()
    {
        
    }
    
    public virtual void TriggerLose()
    {
        
    }
    
    public virtual void TriggerWin()
    {
        
    }

    public virtual void CheckWinCondition()
    {
        
    }

    public virtual async UniTask RestartLevel()
    {
        await UniTask.Yield();
    }
    
    public void OnLoadDone(LevelManager levelManager)
    {
        transform.SetParent(levelManager.transform);
    }

    public Sprite GetHint()
    {
        return (levelDataConfig.SprHint) ? LevelDataConfig.SprHint : null;
    }
}

public class AvoidDangerousObjectLevel : LevelBase
{
    protected async UniTask CountdownTime()
    {
        cancellationTokenSource = new CancellationTokenSource();
        try
        {
            await UniTask.Delay(Utilities.ConvertFloatToTimeSpan(levelDataConfig.Time), 
                cancellationToken: cancellationTokenSource.Token);
            TriggerWin();
        }
        catch (OperationCanceledException)
        {
            Debug.Log("Countdown was cancelled.");
        }
    }
    
    public override void TriggerWin()
    {
        base.TriggerWin();
        LevelManager.Ins.SetLevelPassed();
        ShowUIWin().Forget();
    }

    protected virtual async UniTask ShowUIWin()
    {
        await UniTask.Delay(Utilities.ConvertFloatToTimeSpan(
            GamePlayGlobalDataConfig.Instance.TimeDelayShowUIWin));
        UIManager.Ins.GetUI<CanvasWin>(ECanvasID.CanvasWin).Open();
    }
    
    public override void TriggerLose()
    {
        base.TriggerLose();
        cancellationTokenSource?.Cancel();
        RestartLevel().Forget();
    }
}

