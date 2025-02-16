using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Level5 : AvoidDangerousObjectLevel
{
    [SerializeField] private ProtectedObject protectedObject;
    [SerializeField] private DangerousObject dangerousObject;
    
    public override void OnAfterDrawing()
    {
        protectedObject.OnAfterDrawing();
        dangerousObject.OnAfterDrawing();
        CountdownTime().Forget();
    }

    public override UniTask RestartLevel()
    {
        protectedObject.Restart();
        dangerousObject.OnAfterDrawing();
        return base.RestartLevel();
    }
}
