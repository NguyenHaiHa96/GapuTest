using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Level8 : AvoidDangerousObjectLevel
{
    [SerializeField] private List<MovingDangerousObject> movingDangerousObjects;

    public override void OnAfterDrawing()
    {
        CountdownTime().Forget();
    }
}
