using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DrawingLine : CachedMonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private EdgeCollider2D edgeCollider2D;
    [SerializeField] private float edgeRadius;
    [SerializeField] private float drawingThreshold;
    [SerializeField] private List<Vector2> listPoints = new List<Vector2>();
    [SerializeField] private AudioSource audioSource;
    
    private Vector2 _currentPoint;
    private Vector2 _previousPoint;
    
    public void ClearDrawing()
    {
        enabled = true;
        rb2D.bodyType = RigidbodyType2D.Kinematic;
        WorldPosition = Vector3.zero;
        Rotation = Quaternion.identity;
        listPoints.Clear();
        rb2D.simulated = false;
        lineRenderer.positionCount = 0;
        // edgeCollider2D.isTrigger = true;
        edgeCollider2D.points = Array.Empty<Vector2>();
        Debug.Log("Length: " + edgeCollider2D.points.Length);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            audioSource.Play();
            listPoints.Clear();
            lineRenderer.positionCount = 0;
        }

        if (Input.GetMouseButtonUp(0))
        {
            edgeCollider2D.SetPoints(listPoints);
            rb2D.simulated = true;
            enabled = false;
            if (listPoints.Count > 1)
            {
                EndDrawing();
            }
        }

        if (!Input.GetMouseButton(0)) return;
        _currentPoint = LevelManager.Ins.MainCam.ScreenToWorldPoint(Input.mousePosition);
        if (!(Vector2.Distance(_previousPoint, _currentPoint) > drawingThreshold)) return;
        listPoints.Add(_currentPoint);
        lineRenderer.positionCount = listPoints.Count;
        lineRenderer.SetPosition(listPoints.Count - 1, _currentPoint);
        _previousPoint = _currentPoint;
    }

    private void EndDrawing()
    {
        rb2D.bodyType = RigidbodyType2D.Dynamic;
        edgeCollider2D.isTrigger = false;
        edgeCollider2D.edgeRadius = edgeRadius;
        edgeCollider2D.SetPoints(listPoints);
        LevelManager.Ins.OnAfterDrawing();
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }
}
