using System;
using System.Linq;
using UnityEngine;
using static MaxSdkCallbacks;

public class ApplovinManager : MonoBehaviourSingleton<ApplovinManager>
    , IReadData
{

    private const string SDK_KEY = "dQ15CD6nC7CfuD2IKhScGfRyQOoJpENkqUqftd_Xg0z83xvbcqZKQG3JTTbzUAaR8bGxPGTBufsv3sqxsXzrcV";

    private bool _isNoAdsPurchased;
    public bool IsNoAdsPurchased => _isNoAdsPurchased;

    public void LoadData()
    {
        _isNoAdsPurchased = Data.ReadData.LoadData(GlobalKey.NO_ADS, false);
    }

    public void NoAds()
    {
        _isNoAdsPurchased = true;
        Data.WriteData.Save(GlobalKey.NO_ADS, true);
        var removers = FindObjectsOfType<MonoBehaviour>(true).OfType<IBoughtRemoveAds>();
        removers.ToList().ForEach(i => i.BoughtRemoveAds());
    }

    public void Init()
    {
        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
        {
            InitializeBannerAds();
            InitializeInterstitialAds();
            InitializeRewardedAds();
        };

        // Show AppOpenAd
        //        MaxSdkCallbacks.OnSdkInitializedEvent += sdkConfiguration =>
        //        {
        //            MaxSdkCallbacks.AppOpen.OnAdLoadedEvent += delegate {
        //#if UNITY_EDITOR
        //                MaxSdkUnityEditor.ShowAppOpenAd(AppOpenAdUnitId);
        //            };
        //            MaxSdkUnityEditor.LoadAppOpenAd(AppOpenAdUnitId);
        //        };
        //#else
        //            MaxSdk.ShowAppOpenAd(AppOpenAdUnitId); };
        //            MaxSdk.LoadAppOpenAd(AppOpenAdUnitId);
        //        };
        //#endif

        // Attach callbacks based on the ad format(s) you are using

        MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
        MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
        MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;

#if UNITY_EDITOR
        MaxSdkUnityEditor.SetSdkKey(SDK_KEY);
        //MaxSdkUnityEditor.SetUserId(USER_ID);
        MaxSdkUnityEditor.InitializeSdk();
#else
        MaxSdk.SetSdkKey(SDK_KEY);
        //MaxSdk.SetUserId("USER_ID");
        MaxSdk.InitializeSdk();
#endif
    }

    private void OnAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo impressionData)
    {
        double revenue = impressionData.Revenue;
#if FIREBASE_ANALYTICS
        var impressionParameters = new[] {
        new Firebase.Analytics.Parameter("ad_platform", "AppLovin"),
        new Firebase.Analytics.Parameter("ad_source", impressionData.NetworkName),
        new Firebase.Analytics.Parameter("ad_unit_name", impressionData.AdUnitIdentifier),
        new Firebase.Analytics.Parameter("ad_format", impressionData.AdFormat),
        new Firebase.Analytics.Parameter("value", revenue),
        new Firebase.Analytics.Parameter("currency", "USD"), // All AppLovin revenue is sent in USD
        };
        Firebase.Analytics.FirebaseAnalytics.LogEvent("ad_impression", impressionParameters);
#endif
    }

    #region Banner
#if UNITY_IOS
    readonly string bannerAdUnitId = "YOUR_IOS_BANNER_AD_UNIT_ID"; // Retrieve the ID from your account
#else // UNITY_ANDROID
    readonly string bannerAdUnitId = "51f0c755158ec7d1"; // Retrieve the ID from your account
#endif
    bool isShowingBanner;

    private void InitializeBannerAds()
    {
        // Adaptive banners are sized based on device width for positions that stretch full width (TopCenter and BottomCenter).
        // You may use the utility method `MaxSdkUtils.GetAdaptiveBannerHeight()` to help with view sizing adjustments
#if UNITY_EDITOR
        MaxSdkUnityEditor.CreateBanner(bannerAdUnitId, MaxSdkBase.BannerPosition.BottomCenter);
        MaxSdkUnityEditor.SetBannerExtraParameter(bannerAdUnitId, "adaptive_banner", "true");
        MaxSdkUnityEditor.StartBannerAutoRefresh(bannerAdUnitId);

        // Set background or background color for banners to be fully functional
        MaxSdkUnityEditor.SetBannerBackgroundColor(bannerAdUnitId, Color.black);
#else
        MaxSdk.CreateBanner(bannerAdUnitId, MaxSdkBase.BannerPosition.BottomCenter);
        MaxSdk.SetBannerExtraParameter(bannerAdUnitId, "adaptive_banner", "true"); 
        MaxSdk.StartBannerAutoRefresh(bannerAdUnitId);

        // Set background or background color for banners to be fully functional
        MaxSdk.SetBannerBackgroundColor(bannerAdUnitId, Color.black);
#endif

        MaxSdkCallbacks.Banner.OnAdLoadedEvent += OnBannerAdLoadedEvent;
        MaxSdkCallbacks.Banner.OnAdLoadFailedEvent += OnBannerAdLoadFailedEvent;
        MaxSdkCallbacks.Banner.OnAdClickedEvent += OnBannerAdClickedEvent;
        MaxSdkCallbacks.Banner.OnAdExpandedEvent += OnBannerAdExpandedEvent;
        MaxSdkCallbacks.Banner.OnAdCollapsedEvent += OnBannerAdCollapsedEvent;
    }

#if UNITY_EDITOR
    public void ShowBanner() => MaxSdkUnityEditor.ShowBanner(bannerAdUnitId);

    public void HideBanner() => MaxSdkUnityEditor.HideBanner(bannerAdUnitId);
#else
    public void ShowBanner() => MaxSdk.ShowBanner(bannerAdUnitId);

    public void HideBanner() => MaxSdk.HideBanner(bannerAdUnitId);
#endif

    private void OnBannerAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnBannerAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo) { }

    private void OnBannerAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnBannerAdExpandedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnBannerAdCollapsedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }
    #endregion

    #region Interstitial
#if UNITY_IOS
    readonly string interstitialAdUnitId = "YOUR_IOS_AD_UNIT_ID";
#else // UNITY_ANDROID
    readonly string interstitialAdUnitId = "a116ba829af8240c";
#endif
    int interstitialRetryAttempt;
    bool isShowWhenOpen = false;
    private void InitializeInterstitialAds()
    {
        // Attach callback
        MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialLoadFailedEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;
        MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialAdFailedToDisplayEvent;

        // Load the first interstitial
        LoadInterstitial();
    }

#if UNITY_EDITOR
    private void LoadInterstitial()
    {
        MaxSdkUnityEditor.LoadInterstitial(interstitialAdUnitId);
    }

    public void ShowInterstitial()
    {
        if (_isNoAdsPurchased) return;
        if (MaxSdkUnityEditor.IsInterstitialReady(interstitialAdUnitId))
        {
            MaxSdkUnityEditor.ShowInterstitial(interstitialAdUnitId);
        }
    }
#else
    private void LoadInterstitial()
    {
        MaxSdk.LoadInterstitial(interstitialAdUnitId);
    }

    public void ShowInterstitial()
    {
        if(_isNoAdsPurchased) return;
        if (MaxSdk.IsInterstitialReady(interstitialAdUnitId))
        {
            MaxSdk.ShowInterstitial(interstitialAdUnitId);
        }
    }
#endif

    private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is ready for you to show. MaxSdk.IsInterstitialReady(adUnitId) now returns 'true'
        if (!isShowWhenOpen)
        {
            isShowWhenOpen = true;
            ShowInterstitial();
        }

        // Reset retry attempt
        interstitialRetryAttempt = 0;
    }

    private void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Interstitial ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds)

        interstitialRetryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, interstitialRetryAttempt));

        Invoke(nameof(LoadInterstitial), (float)retryDelay);
    }

    private void OnInterstitialDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnInterstitialAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad failed to display. AppLovin recommends that you load the next ad.
        LoadInterstitial();
    }

    private void OnInterstitialClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is hidden. Pre-load the next ad.
        LoadInterstitial();
    }
    #endregion

    #region Rewarded
#if UNITY_IOS
    readonly string rewardedAdUnitId = "YOUR_IOS_AD_UNIT_ID";
#else // UNITY_ANDROID
    readonly string rewardedAdUnitId = "da57dd4d3c69e43b";
#endif
    int rewardedRetryAttempt;
    Action onRewarded;

    public void InitializeRewardedAds()
    {
        // Attach callback
        MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdLoadFailedEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHiddenEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

        // Load the first rewarded ad
        LoadRewardedAd();
    }


#if UNITY_EDITOR
    private void LoadRewardedAd()
    {
        MaxSdkUnityEditor.LoadRewardedAd(rewardedAdUnitId);
    }

    public void ShowRewardedAd(Action _onReawarded)
    {
        if(_isNoAdsPurchased)
        {
            _onReawarded.Invoke();
            return;
        }
        if (MaxSdkUnityEditor.IsRewardedAdReady(rewardedAdUnitId))
        {
            onRewarded = _onReawarded;
            MaxSdkUnityEditor.ShowRewardedAd(rewardedAdUnitId);
            return;
        }
    }
#else
    private void LoadRewardedAd()
    {
        MaxSdk.LoadRewardedAd(rewardedAdUnitId);
    }

    public void ShowRewardedAd(Action _onReawarded)
    {
        if(_isNoAdsPurchased)
        {
            _onReawarded.Invoke();
            return;
        }
        if (MaxSdk.IsRewardedAdReady(rewardedAdUnitId))
        {
            onRewarded = _onReawarded;
            MaxSdk.ShowRewardedAd(rewardedAdUnitId);
            return;
        }
    }
#endif


    private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is ready for you to show. MaxSdk.IsRewardedAdReady(adUnitId) now returns 'true'.

        // Reset retry attempt
        rewardedRetryAttempt = 0;
    }

    private void OnRewardedAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Rewarded ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds).

        rewardedRetryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, rewardedRetryAttempt));

        Invoke(nameof(LoadRewardedAd), (float)retryDelay);
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad failed to display. AppLovin recommends that you load the next ad.
        LoadRewardedAd();
    }

    private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnRewardedAdHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is hidden. Pre-load the next ad
        LoadRewardedAd();
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
    {
        // The rewarded ad displayed and the user should receive the reward.
        onRewarded.Invoke();
    }
    #endregion
}
