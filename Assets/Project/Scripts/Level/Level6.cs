using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Level6 : AvoidDangerousObjectLevel
{
    [SerializeField] private List<MoveWithDirectionDangerousObject> movingDangerousObjects;

    public override void OnAfterDrawing()
    {
        for (int i = 0; i < movingDangerousObjects.Count; i++)
        {
            MoveWithDirectionDangerousObject moveWithDirectionDangerousObject = movingDangerousObjects[i];
            moveWithDirectionDangerousObject.OnAferDrawing();
        }
        CountdownTime().Forget();
    }

    public override UniTask RestartLevel()
    {
        for (int i = 0; i < movingDangerousObjects.Count; i++)
        {
            MoveWithDirectionDangerousObject moveWithDirectionDangerousObject = movingDangerousObjects[i];
            moveWithDirectionDangerousObject.Restart();
        }
        return base.RestartLevel();
    }
}
