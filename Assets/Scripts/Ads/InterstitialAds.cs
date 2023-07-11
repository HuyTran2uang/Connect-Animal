using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Applovin
{
    public class InterstitialAds : MonoBehaviourSingleton<InterstitialAds>
    {
#if UNITY_EDITOR
        string adUnitId = "YOUR_editor_AD_UNIT_ID";
#elif UNITY_IOS
        string adUnitId = "YOUR_IOS_AD_UNIT_ID";
#elif UNITY_ANDROID
        string adUnitId = "a116ba829af8240c";
#endif
        int retryAttempt;

        public void InitializeInterstitialAds()
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

        private void LoadInterstitial()
        {
            Debug.Log("Load Interstitial ads");
            MaxSdk.LoadInterstitial(adUnitId);
        }

        public void Show()
        {
            Debug.Log("Start Show Interstitial ads");
            if (MaxSdk.IsInterstitialReady(adUnitId))
            {
                Debug.Log("Showing Interstitial Ads");
                MaxSdk.ShowInterstitial(adUnitId);
            }
            else
            {
                Debug.Log("Interstitial ads is not ready!!!!");
            }
        }

        private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Interstitial ad is ready for you to show. MaxSdk.IsInterstitialReady(adUnitId) now returns 'true'

            // Reset retry attempt
            retryAttempt = 0;
        }

        private void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            // Interstitial ad failed to load 
            // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds)

            retryAttempt++;
            double retryDelay = Mathf.Pow(2, Mathf.Min(6, retryAttempt));

            Invoke("LoadInterstitial", (float)retryDelay);
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
    }
}