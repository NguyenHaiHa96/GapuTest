using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDangerousObject : ObjectInScene
{
     public override void OnAfterDrawing()
     {
          base.OnAfterDrawing();
          rb2D.bodyType = RigidbodyType2D.Dynamic;
     }

     public override void Restart()
     {
          base.Restart();
          rb2D.bodyType = RigidbodyType2D.Kinematic; 
     }
}
