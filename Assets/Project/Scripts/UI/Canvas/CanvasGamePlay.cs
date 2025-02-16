using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using R3;
using UnityEngine.Serialization;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] private TextMeshProUGUI tmpKeyAmount;
    [SerializeField] private TextMeshProUGUI tmpLevel;
    [SerializeField] private TextMeshProUGUI tmpCountdown;
    [SerializeField] private Transform tfWinCheck;
    [SerializeField] private Transform tfLoseX;
    [SerializeField] private Button btnBack;
    
    [BoxGroup("Bottom Panel")]
    [SerializeField] private Button btnHint;
    [BoxGroup("Bottom Panel")]
    [SerializeField] private GameObject goKeyConsume;
    [BoxGroup("Bottom Panel")]
    [SerializeField] private Image imgAdsHint;
    [BoxGroup("Bottom Panel")]
    [SerializeField] private TextMeshProUGUI tmpKeyConsume;
    [BoxGroup("Bottom Panel")]
    [SerializeField] private Button btnEarnKey;
    [BoxGroup("Bottom Panel")]
    [SerializeField] private Button btnSkip;

    [BoxGroup("Anim")] 
    [SerializeField] private Vector3 scaleTarget;
    [BoxGroup("Anim")] 
    [SerializeField] private Ease easeShow;
    [BoxGroup("Anim")]
    [SerializeField] private float timeShowWinCheck;
    [BoxGroup("Anim")]
    [SerializeField] private float timeShowLoseX;
    
    [BoxGroup("Popup Hint")]
    [SerializeField] private GameObject goPopupHint;
    [BoxGroup("Popup Hint")] 
    [SerializeField] private Button btnOk;
    [BoxGroup("Popup Hint")] 
    [SerializeField] private Button btnClose;
    [BoxGroup("Popup Hint")] 
    [SerializeField] private Image imgHint;
    
    private CancellationTokenSource _cancelationToken;
    private Tween _tweenShowWinCheck;
    private Tween _tweenShowLoseX;
    private float _countdownTime;
    private int _keyAmount;
    
    private void Start()
    {
        LevelManager.Ins.onAfterDrawingCallback.AddListener(StartCountdown);
        LevelManager.Ins.onLoseCallback.AddListener(ShowLoseX);
        LevelManager.Ins.onWinCallback.AddListener(ShowWinCheck);
        LevelManager.Ins.onNextLevelCallback.AddListener(SetupCanvas);
        UserDataManager.Ins.UserData.KeyAmount.Subscribe(UpdateKeyAmount);
        UserDataManager.Ins.UserData.HintAmount.Subscribe(UpdateButtonHint);
        UpdateTextLevel(UserDataManager.Ins.UserData.StrLastLevel.Value);
        tmpKeyConsume.SetText($"-{GeneralGlobalDataConfig.Instance.KeyEarnedPerAds}");
        btnBack.onClick.AddListener(OnClickBack);
        btnHint.onClick.AddListener(OnClickShowHint);
        btnEarnKey.onClick.AddListener(OnClickEarnKey);
        btnSkip.onClick.AddListener(OnClickSkip);
        btnOk.onClick.AddListener(OnClickClosePopupHint);
        btnClose.onClick.AddListener(OnClickClosePopupHint);
        SetupCanvas();
    }

    public void SetupCanvas()
    {
        tfWinCheck.localScale = Vector3.zero;
        tfLoseX.localScale = Vector3.zero;
        tmpCountdown.gameObject.SetActive(false);
        UpdateTextLevel(UserDataManager.Ins.UserData.StrLastLevel.Value);
    }
    
    public override void Close()
    {
        _tweenShowWinCheck?.Kill();
        _tweenShowLoseX?.Kill();
        base.Close();
    }

    private void StartCountdown()
    {
        _cancelationToken = new CancellationTokenSource();
        _countdownTime = LevelManager.Ins.GetTimeCountdown();
        tmpCountdown.gameObject.SetActive(true);
        Countdown(_cancelationToken.Token).Forget();
    }
    
    private async UniTask Countdown(CancellationToken token)
    {
        float remainingTime = _countdownTime;
        while (remainingTime > 0)
        {
            tmpCountdown.SetText($"{Mathf.CeilToInt(remainingTime)}");
            await UniTask.Delay(Utilities.ConvertFloatToTimeSpan(1), cancellationToken: token);
            remainingTime--;
        }
        tmpCountdown.SetText(Constants.STR_0);
        StopCountdown();
    }

    private void StopCountdown()
    {
        _cancelationToken?.Cancel();
    }

    private void ShowWinCheck()
    {
        _tweenShowWinCheck = tfWinCheck.DOScale(scaleTarget, timeShowWinCheck).SetEase(easeShow);
    }
    
    private void ShowLoseX()
    {
        StopCountdown();
        _tweenShowLoseX = tfLoseX.DOScale(scaleTarget, timeShowLoseX).SetEase(easeShow);
        ResetCountdown();
        HideLoseX().Forget();
    }

    private async UniTask HideLoseX()
    {
        await UniTask.Delay(Utilities.ConvertFloatToTimeSpan(timeShowLoseX));
        _tweenShowLoseX?.Kill();
        tfLoseX.localScale = Vector3.zero;
    }
    
    private void UpdateKeyAmount(int amount)
    {
        _keyAmount = amount;
        tmpKeyAmount.SetText($"{_keyAmount}");
        if (_keyAmount > 0)
        {
            goKeyConsume.SetActive(true);
            imgAdsHint.gameObject.SetActive(false);
        }
        else
        {
            goKeyConsume.SetActive(false);
            imgAdsHint.gameObject.SetActive(true);
        }
    }
    
    private void UpdateTextLevel(string level)
    {
        string tmp = level.Replace(Constants.STR_LEVEL, 
            Constants.STR_LEVEL_FORMATTED).TrimStart('0');
        tmpLevel.SetText($"{tmp}");
    }
    
    private void OnClickBack()
    {
        Close();
        LevelManager.Ins.DeleteCurrentLevel();
        GameManager.Ins.ChangeState(EGameState.SelectLevel);
    }

    private void UpdateButtonHint(int amount)
    {
        if (UserDataManager.Ins.HaveAvailableKey())
        {
            goKeyConsume.gameObject.SetActive(true);
            imgAdsHint.gameObject.SetActive(false);
        }
        else
        {
            goKeyConsume.gameObject.SetActive(false);
            imgAdsHint.gameObject.SetActive(true);
        }
    }
    
    private void OnClickShowHint()
    {
        if (UserDataManager.Ins.HaveAvailableKey())
        {
            UserDataManager.Ins.ConsumeKey
                (GeneralGlobalDataConfig.Instance.KeyConsumePerHint);
            ShowHint();
        }
        else
        {
            //TODO: Show ads
            ShowHint();
        }
    }

    private void ShowHint()
    {
        imgHint.sprite = LevelManager.Ins.GetCurrentLevelHint();
        goPopupHint.SetActive(true);
    }
    
    private void OnClickEarnKey()
    {
        // TODO: Show ads
        UserDataManager.Ins.EarnKey(GeneralGlobalDataConfig.Instance.KeyEarnedPerAds);
    }
    
    private void OnClickSkip()
    {
        //TODO: Skip level
    }

    private void OnClickClosePopupHint()
    {
        goPopupHint.SetActive(false);
    }
    
    public void ResetCountdown()
    {
        SetupCanvas();
        _countdownTime = LevelManager.Ins.GetTimeCountdown();
    }
}
