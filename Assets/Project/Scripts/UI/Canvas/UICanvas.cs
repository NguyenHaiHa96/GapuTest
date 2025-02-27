﻿using UnityEngine;

public class UICanvas : MonoBehaviour
{
    //public bool IsAvoidBackKey = false;
    public bool isDestroyOnClose = false;
    protected RectTransform rectTransform;
    private Animator animatior;
    // private bool m_IsInit = false;
    // private float m_OffsetY = 0;

    private void Start()
    {
        Init();
    }

    protected void Init()
    {
        rectTransform = GetComponent<RectTransform>();
        animatior = GetComponent<Animator>();

        //float ratio = (float)Screen.height / (float)Screen.width;

        //// xu ly tai tho
        //if (ratio > 2.1f)
        //{
        //    Vector2 leftBottom = m_RectTransform.offsetMin;
        //    Vector2 rightTop = m_RectTransform.offsetMax;
        //    rightTop.y = -100f;
        //    m_RectTransform.offsetMax = rightTop;
        //    leftBottom.y = 0f;
        //    m_RectTransform.offsetMin = leftBottom;
        //    m_OffsetY = 100f;
        //}
        //m_IsInit = true;
    }

    public virtual void Setup()
    {
        UIManager.Ins.AddBackUI(this);
        UIManager.Ins.PushBackAction(this, BackKey);
    }

    public virtual void BackKey()
    {

    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
        //anim
    }

    public virtual void Close()
    {
        UIManager.Ins.RemoveBackUI(this);
        //anim
        gameObject.SetActive(false);
        if (isDestroyOnClose)
        {
            Destroy(gameObject);
        }
    }
}
