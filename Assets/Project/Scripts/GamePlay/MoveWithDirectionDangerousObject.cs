using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithDirectionDangerousObject : ObjectInScene
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float moveSpeed;
    
    protected Vector3 startRotation;
    private bool _isMoving;

    protected override void Setup()
    {
        base.Setup();
        startRotation = EulerAngles;
    }

    public void OnAferDrawing()
    {
        _isMoving = true;
    }
    
    private void Update()
    {
        if (!_isMoving) return;
        rb2D.velocity = direction * moveSpeed;
    }

    public override void Restart()
    {
        base.Restart();
        RestartPosition();
        EulerAngles = startRotation;
        rb2D.velocity = Vector2.zero;
        _isMoving = false;
    }
    
#if UNITY_EDITOR
    
    [SerializeField] private float length = 1f;
    [SerializeField] private Color gizmoColor = Color.red;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + (direction.normalized * length);
        Gizmos.DrawLine(startPos, endPos);
        Gizmos.DrawSphere(endPos, 0.1f);
    }
    
#endif
}
