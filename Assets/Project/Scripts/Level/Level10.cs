using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Level10 : AvoidDangerousObjectLevel
{
    [SerializeField] private MovingProtectedObject movingProtectedObject;
    
     public override void OnAfterDrawing()
    {
        movingProtectedObject.OnAfterDrawing();
        CountdownTime().Forget();
    }
}
