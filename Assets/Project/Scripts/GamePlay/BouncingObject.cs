using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BouncingObject : ObjectInScene
{
    [SerializeField] private Vector3 bounceCoordinate;
    [SerializeField] private float bounceForce;
    
    public Vector3 BounceCoordinate => bounceCoordinate;
    public float BounceForce => bounceForce;

    public Vector2 GetBounceForce()
    {
        Vector2 direction = (bounceCoordinate - WorldPosition).normalized;
        return direction * bounceForce;
    }

    #region Editor

#if UNITY_EDITOR
    
    [SerializeField] private float length = 1f;
    [SerializeField] private Color gizmoColor = Color.red;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + (Vector3)(bounceCoordinate.normalized * length);
        Gizmos.DrawLine(startPos, endPos);
        Gizmos.DrawSphere(endPos, 0.1f);
    }
    
#endif

    #endregion
    
}
