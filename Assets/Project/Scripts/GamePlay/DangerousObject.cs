using UnityEngine;

public class DangerousObject : ObjectInScene
{
    public override void OnAfterDrawing()
    {
        if (!rb2D) return;
        rb2D.bodyType = RigidbodyType2D.Dynamic;
    }

    public override void Restart()
    {
        base.Restart();
        RestartPosition();
        rb2D.bodyType = RigidbodyType2D.Kinematic;
    }
}
