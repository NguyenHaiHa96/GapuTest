using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

[CreateAssetMenu(fileName = "GeneraGlobalDataConfig",
    menuName = "Scriptable Objects/General Global Data Config")]
public class GeneralGlobalDataConfig : GlobalConfig<GeneralGlobalDataConfig>
{
    [SerializeField] private int keyConsumePerHint;
    [SerializeField] private int keyEarnedPerAds;
    
    public int KeyConsumePerHint => keyConsumePerHint;
    public int KeyEarnedPerAds => keyEarnedPerAds;
}
