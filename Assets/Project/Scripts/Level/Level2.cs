using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Level2 : AvoidDangerousObjectLevel
{
    [SerializeField] private ProtectedObject protectedObject;
    [SerializeField] private DangerousObject dangerousObject;
    
    public override void OnAfterDrawing()
    {
        protectedObject.OnAfterDrawing();
        CountdownTime().Forget();
    }

    public override UniTask RestartLevel()
    {
        protectedObject.Restart();
        dangerousObject.Restart();
        return base.RestartLevel();
    }
}
