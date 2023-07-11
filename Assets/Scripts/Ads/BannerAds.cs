using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Applovin
{
    public class BannerAds : MonoBehaviourSingleton<BannerAds>
    {
#if UNITY_EDITOR
        string bannerAdUnitId = "YOUR_editor_BANNER_AD_UNIT_ID";
#elif UNITY_IOS
        string bannerAdUnitId = "YOUR_IOS_BANNER_AD_UNIT_ID"; // Retrieve the ID from your account
#elif UNITY_ANDROID
        string bannerAdUnitId = "51f0c755158ec7d1"; // Retrieve the ID from your account
#endif

        public void InitializeBannerAds()
        {
            // Banners are automatically sized to 320×50 on phones and 728×90 on tablets
            // You may call the utility method MaxSdkUtils.isTablet() to help with view sizing adjustments
            MaxSdk.CreateBanner(bannerAdUnitId, MaxSdkBase.BannerPosition.BottomCenter);
            MaxSdk.SetBannerExtraParameter(bannerAdUnitId, "adaptive_banner", "true");


            // Set background or background color for banners to be fully functional
            MaxSdk.SetBannerBackgroundColor(bannerAdUnitId, Color.white);

            MaxSdkCallbacks.Banner.OnAdLoadedEvent += OnBannerAdLoadedEvent;
            MaxSdkCallbacks.Banner.OnAdLoadFailedEvent += OnBannerAdLoadFailedEvent;
            MaxSdkCallbacks.Banner.OnAdClickedEvent += OnBannerAdClickedEvent;
            MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnBannerAdRevenuePaidEvent;
            MaxSdkCallbacks.Banner.OnAdExpandedEvent += OnBannerAdExpandedEvent;
            MaxSdkCallbacks.Banner.OnAdCollapsedEvent += OnBannerAdCollapsedEvent;
        }

        public void Show()
        {
            Debug.Log("Show banner ads");
            MaxSdk.ShowBanner(bannerAdUnitId);
        }

        public void Hide()
        {
            MaxSdk.HideBanner(bannerAdUnitId);
        }

        private void OnBannerAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

        private void OnBannerAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo) { }

        private void OnBannerAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

        private void OnBannerAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

        private void OnBannerAdExpandedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

        private void OnBannerAdCollapsedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }
    }
}
