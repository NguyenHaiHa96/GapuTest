using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CachedMonoBehaviour : MonoBehaviour
{
    private Transform _tf;
    private RectTransform _rectTf;
    public Transform Transform
    {
        get
        {
            if (_tf == null)
            {
                _tf = gameObject.transform;
            }
            return _tf;
        }
    }

    public RectTransform RectTf
    {
        get
        {
            if (_rectTf != null) return _rectTf;
            _rectTf = gameObject.GetComponent<RectTransform>(); ;
            return _rectTf;
        }
    }

    public Quaternion Rotation { get => Transform.rotation; set => Transform.rotation = value; }
    public Vector3 WorldPosition { get => Transform.position; set => Transform.position = value; }
    public Vector3 LocalPosition { get => Transform.localPosition; set => Transform.localPosition = value; }
    public Vector3 LocalScale { get => Transform.localScale; set => Transform.localScale = value; }
    public Vector3 EulerAngles { get => Transform.eulerAngles; set => transform.eulerAngles = value; }
    public Vector3 EulerLocalRotation => Transform.localRotation.eulerAngles;
    public Vector3 AnchoredPosition { get => RectTf.anchoredPosition; set => RectTf.anchoredPosition = value; }
    public Vector3 SizeDelta { get => RectTf.sizeDelta; set => RectTf.sizeDelta = value; }

    public float DeltaTime => Time.deltaTime;
    public float FixedDeltaTime => Time.fixedDeltaTime;
}
