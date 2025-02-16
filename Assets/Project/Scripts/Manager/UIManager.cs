using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ECanvasID
{
    None = 0,
    CanvasGamePlay = 1,
    CanvasBlockRaycast = 2,
    CanvasMainMenu = 3,
    CanvasSetting = 4,
    CanvasLose = 5,
    CanvasWin = 6,
    CanvasSelectLevel = 7,
}

public class UIManager : Singleton<UIManager>
{
    public Transform tfCanvasParent;

    private Dictionary<ECanvasID, UICanvas> _uiCanvasDict = new Dictionary<ECanvasID, UICanvas>();

    #region Canvas

    public bool IsOpenedUI(ECanvasID id)
    {
        return _uiCanvasDict.ContainsKey(id) && _uiCanvasDict[id] != null &&
               _uiCanvasDict[id].gameObject.activeInHierarchy;
    }

    public UICanvas GetUI(ECanvasID id)
    {
        if (!_uiCanvasDict.ContainsKey(id) || _uiCanvasDict[id] == null)
        {
            UICanvas canvas = Instantiate(Resources.Load<UICanvas>("UI/" + id.ToString()), tfCanvasParent);
            _uiCanvasDict[id] = canvas;
        }

        return _uiCanvasDict[id];
    }

    public T GetUI<T>(ECanvasID id) where T : UICanvas
    {
        return GetUI(id) as T;
    }

    public UICanvas OpenUI(ECanvasID id)
    {
        UICanvas canvas = GetUI(id);

        canvas.Setup();
        canvas.Open();

        return canvas;
    }

    public T OpenUI<T>(ECanvasID id) where T : UICanvas
    {
        return OpenUI(id) as T;
    }

    public bool IsOpened(ECanvasID id)
    {
        return _uiCanvasDict.ContainsKey(id) && _uiCanvasDict[id] != null;
    }

    #endregion

    #region Back Button

    private Dictionary<UICanvas, UnityAction> _backActionEvents = new Dictionary<UICanvas, UnityAction>();
    private List<UICanvas> _backCanvas = new List<UICanvas>();
    UICanvas BackTopUI
    {
        get
        {
            UICanvas canvas = null;
            if (_backCanvas.Count > 0)
            {
                canvas = _backCanvas[^1];
            }

            return canvas;
        }
    }

    public void PushBackAction(UICanvas canvas, UnityAction action)
    {
        if (!_backActionEvents.ContainsKey(canvas))
        {
            _backActionEvents.Add(canvas, action);
        }
    }

    public void AddBackUI(UICanvas canvas)
    {
        if (!_backCanvas.Contains(canvas))
        {
            _backCanvas.Add(canvas);
        }
    }

    public void RemoveBackUI(UICanvas canvas)
    {
        _backCanvas.Remove(canvas);
    }

    /// <summary>
    /// CLear back key when comeback index UI canvas
    /// </summary>
    public void ClearBackKey()
    {
        _backCanvas.Clear();
    }

    #endregion

    public void CloseUI(ECanvasID id)
    {
        if (IsOpened(id))
        {
            GetUI(id).Close();
        }
    }
}