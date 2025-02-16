using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
using R3;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource soundAudioSource;
    [SerializeField] private AssetReference assetReferenceAudioDataConfig;
    [SerializeField] private AudioReferenceDataConfig dataConfig;
    [SerializeField] private List<AudioSource> audioSourcesInScene;
    
    private bool _isSoundActive;
    private bool _isMusicActive;
    
    private void Start()
    {
        LoadAudioReferenceDataConfig().Forget();
    }
    
    
    public void Setup()
    {
        UserDataManager.Ins.UserData.SoundActive.Subscribe(SetSoundFx);
        UserDataManager.Ins.UserData.MusicActive.Subscribe(SetMusic);
    }
    
    private async UniTask LoadAudioReferenceDataConfig()
    {
        var handle = Addressables.LoadAssetAsync<AudioReferenceDataConfig>(assetReferenceAudioDataConfig);
        dataConfig = await handle.ToUniTask();
        PlayMusicAudio(EAudioType.SfxMainMenu);
    }
    
    public void LoadAllAudioSourcesInScene()
    {
        audioSourcesInScene = new List<AudioSource>(FindObjectsOfType<AudioSource>());
        if (audioSourcesInScene.Count <= 0) return;
        foreach (var audioSource in audioSourcesInScene)
        {
            audioSource.mute = !_isSoundActive;
        }
    }

    private void PlayMusicAudio(EAudioType audioType)
    {
        musicAudioSource.clip = dataConfig.GetAudioClip(audioType);
        musicAudioSource.Play();
    }
    
    public void PlaySoundFxAudtion(EAudioType audioType)
    {
        soundAudioSource.clip = dataConfig.GetAudioClip(audioType);
        soundAudioSource.Play();
    }
    
    private void SetSoundFx(bool isOn)
    {
        _isSoundActive = isOn;
        musicAudioSource.mute = !_isSoundActive;
    }
    
    private void SetMusic(bool isOn)
    {
        _isMusicActive = isOn;
        musicAudioSource.mute = !_isMusicActive;
    }
}

public enum EAudioType
{
    None = 0,
    SfxMainMenu = 1,
    SfxGamePlay = 2,
    SfxWin = 3,
    SfxLose = 4,
}