using Applovin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if APPLOVIN
public class AdsManager : MonoBehaviourSingleton<AdsManager>, IAfterPrepareGame
{
    public void InitializeAds()
    {
        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) => {
            // AppLovin SDK is initialized, start loading ads
            BannerAds.Instance.InitializeBannerAds();
            InterstitialAds.Instance.InitializeInterstitialAds();
            RewardAds.Instance.InitializeRewardedAds();
        };

        MaxSdk.SetSdkKey("dQ15CD6nC7CfuD2IKhScGfRyQOoJpENkqUqftd_Xg0z83xvbcqZKQG3JTTbzUAaR8bGxPGTBufsv3sqxsXzrcV");
        //MaxSdk.SetUserId("USER_ID");
        MaxSdk.InitializeSdk();
    }

    public void AfterPrepareGame()
    {
        BannerAds.Instance.Show();
        InterstitialAds.Instance.Show();
    }
}
#endif