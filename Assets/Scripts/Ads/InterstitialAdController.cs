using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Sample
{
    /// <summary>
    /// Demonstrates how to use Google Mobile Ads interstitial ads.
    /// </summary>
    [AddComponentMenu("GoogleMobileAds/InterstitialAdController")]
    public class InterstitialAdController : MonoBehaviour
    {

        #region EDITOR FIELDS

        [SerializeField, Range(1, 900)] private int delayBetweenAds = 240;


        #endregion


        #region PRIVATE FIELDS

        private static float lastAdTime = Mathf.NegativeInfinity;

        #endregion


        // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
        private const string _adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        private const string _adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        private const string _adUnitId = "unused";
#endif

        private InterstitialAd _interstitialAd;

        public void LoadAd()
        {
            if (_interstitialAd != null)
            {
                DestroyAd();
            }

            Debug.Log("Loading interstitial ad.");

            var adRequest = new AdRequest();

            InterstitialAd.Load(_adUnitId, adRequest, (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null)
                {
                    Debug.LogError("Interstitial ad failed to load an ad with error : " + error);
                    return;
                }
                if (ad == null)
                {
                    Debug.LogError("Unexpected error: Interstitial load event fired with null ad and null error.");
                    return;
                }
                Debug.Log("Interstitial ad loaded with response : " + ad.GetResponseInfo());
                _interstitialAd = ad;

                RegisterEventHandlers(ad);

            });

            lastAdTime = Time.time;
            Debug.Log("ShowInterstitialWithDelayBetweenAds " + lastAdTime);
        }

        public void ShowAd()
        {
            if (_interstitialAd != null && _interstitialAd.CanShowAd())
            {
                Debug.Log("Showing interstitial ad.");
                _interstitialAd.Show();
            }
            else
            {
                Debug.LogError("Interstitial ad is not ready yet.");
            }

        }

        public void DestroyAd()
        {
            if (_interstitialAd != null)
            {
                Debug.Log("Destroying interstitial ad.");
                _interstitialAd.Destroy();
                _interstitialAd = null;
            }

        }

        public void LogResponseInfo()
        {
            if (_interstitialAd != null)
            {
                var responseInfo = _interstitialAd.GetResponseInfo();
                UnityEngine.Debug.Log(responseInfo);
            }
        }

        public void LoadAndShowInterstitialAd()
        {
            if (PlayerPrefs.GetInt("adsRemoved") == 0)
            {
                if ((Time.time - lastAdTime) > (float)delayBetweenAds)
                {
                    LoadAd();
                    ShowAd();
                }
            }
        }

        private void RegisterEventHandlers(InterstitialAd ad)
        {
            ad.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };      
            ad.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Interstitial ad recorded an impression.");
            };
            ad.OnAdClicked += () =>
            {
                Debug.Log("Interstitial ad was clicked.");
            };
            ad.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("Interstitial ad full screen content opened.");
            };
            ad.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Interstitial ad full screen content closed.");
            };
            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("Interstitial ad failed to open full screen content with error : "
                    + error);
            };
        }
    }
}