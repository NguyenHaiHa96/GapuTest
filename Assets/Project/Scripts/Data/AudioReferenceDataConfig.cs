using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioReferenceDataConfig",
    menuName = "Scriptable Objects/Audio Reference Data Config")]
public class AudioReferenceDataConfig : ScriptableObject
{
   [SerializeField] private List<AudioConfig> audioConfigs;
   
   public AudioClip GetAudioClip(EAudioType audioType)
   {
       return audioConfigs.Find(audioConfig => audioConfig.AudioType == audioType).AudioClip;
   }
}

[Serializable]
public class AudioConfig
{
    [SerializeField] private EAudioType audioType;
    [SerializeField] private AudioClip audioClip;
    
    public EAudioType AudioType => audioType;
    public AudioClip AudioClip => audioClip;
}