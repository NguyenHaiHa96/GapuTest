using UnityEngine;
using Cysharp.Threading.Tasks;

public class Level1 : AvoidDangerousObjectLevel
{
    [SerializeField] private ProtectedObject protectedObject;
    [SerializeField] private DangerousObject dangerousObject;
    
    public override void OnAfterDrawing()
    {
        protectedObject.OnAfterDrawing();
        CountdownTime().Forget();
    }

    public override async UniTask RestartLevel()
    {
        await UniTask.Delay(Utilities.ConvertFloatToTimeSpan
            (GamePlayGlobalDataConfig.Instance.TimeDelayRestartLevel));
        protectedObject.Restart();
    }
}
