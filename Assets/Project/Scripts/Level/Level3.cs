using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Level3 : AvoidDangerousObjectLevel
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
        return base.RestartLevel();
    }
}
