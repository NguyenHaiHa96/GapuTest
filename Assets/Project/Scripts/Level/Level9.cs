using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Level9 : AvoidDangerousObjectLevel
{
    [SerializeField] private ProtectedObject protectedObject;
    [SerializeField] private List<MoveWithDirectionDangerousObject> dangerousObjects;
    
    public override void OnAfterDrawing()
    {
        protectedObject.OnAfterDrawing();
        for (int i = 0; i < dangerousObjects.Count; i++)
        {
            MoveWithDirectionDangerousObject dangerousObject = dangerousObjects[i];
            dangerousObject.OnAferDrawing();
        }
        CountdownTime().Forget();
    }

    public override UniTask RestartLevel()
    {
        protectedObject.Restart();
        return base.RestartLevel();
    }
}
