using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

[CreateAssetMenu(fileName = "GamePlayGlobalDataConfig",
    menuName = "Scriptable Objects/Game Play Global Data Config")]
public class GamePlayGlobalDataConfig : GlobalConfig<GamePlayGlobalDataConfig>
{
    [SerializeField] private float timeDelayShowUIWin;
    [SerializeField] private float timeDelayRestartLevel;
    
    public float TimeDelayShowUIWin => timeDelayShowUIWin;
    public float TimeDelayRestartLevel => timeDelayRestartLevel;
}
