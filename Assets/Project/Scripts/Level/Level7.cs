using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Level7 : AvoidDangerousObjectLevel
{
    [SerializeField] private ProtectedObject protectedObject;
    
    public override void OnAfterDrawing()
    {
        protectedObject.OnAfterDrawing();
        CountdownTime().Forget();
    }

    public override UniTask RestartLevel()
    {
        protectedObject.Restart();
        return base.RestartLevel();
    }
}
