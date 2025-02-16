using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingProtectedObject : ProtectedObject
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float moveSpeed;
    
    private bool _isMoving;
    
    private void Update()
    {
        if (!_isMoving) return;
        rb2D.velocity = direction.normalized * moveSpeed;
    }
    
    public override void OnAfterDrawing()
    {
        rb2D.bodyType = RigidbodyType2D.Dynamic;
        _isMoving = true;
    }
    
    public override void Restart()
    {
        base.Restart();
        _isMoving = false;
        rb2D.velocity = Vector2.zero;
    }
}
