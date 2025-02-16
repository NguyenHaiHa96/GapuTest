using System;
using UnityEngine;

public class ProtectedObject : ObjectInScene
{
    [SerializeField] private Collider2D col2D;
    
    public override void OnAfterDrawing()
    {
        rb2D.bodyType = RigidbodyType2D.Dynamic;
    }

    public override void Restart()
    {
        RestartPosition();
        rb2D.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        CollideWithBouncingObject(other);
        CollideWithDangerousObject(other);
    }

    private void CollideWithBouncingObject(Collision2D other)
    {
        if (!other.gameObject.CompareTag(Utilities.ConvertEnumToString(EGameTags.BouncingObject))) return;
        BouncingObject bouncingObject = other.gameObject.GetComponent<BouncingObject>();
        if (bouncingObject == null) return;
        Bounce(bouncingObject.GetBounceForce());
    }
    
    private void CollideWithDangerousObject(Collision2D other)
    {
        if (!other.gameObject.CompareTag(Utilities.ConvertEnumToString(EGameTags.DangerousObject))) return;
        Debug.Log("Collide With Dangerous Object");
        LevelManager.Ins.TriggerLose();
    }

    private void Bounce(Vector2 force)
    {
        if (rb2D == null) return;
        rb2D.AddForce(force, ForceMode2D.Impulse);
    }
}
