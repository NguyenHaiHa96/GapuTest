using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class ObjectInScene : CachedMonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb2D;
    
    protected Vector3 startPosition;

    private void Start()
    {
        Setup();
    }

    public virtual void OnAfterDrawing()
    {
        
    }
    
    public virtual void Restart()
    {
        
    }

    protected virtual void Setup()
    {
        startPosition = WorldPosition;
    }
    
    protected virtual void RestartPosition()
    {
        WorldPosition = startPosition;
    }
}
