using Applovin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviourSingleton<AdsManager>, IPrepareGame
{
    private void Awake()
    {
        BannerAds.Instance.InitializeBannerAds();
        InterstitialAds.Instance.InitializeInterstitialAds();
        RewardAds.Instance.InitializeRewardedAds();
    }

    public void Prepare()
    {
        InterstitialAds.Instance.Show();
    }
}
