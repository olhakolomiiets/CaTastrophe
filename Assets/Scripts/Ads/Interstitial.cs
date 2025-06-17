using Firebase.Analytics;
using GoogleMobileAds.Sample;
using UnityEngine;

public class Interstitial : MonoBehaviour
{

    #region EDITOR FIELDS

    [SerializeField] private InterstitialAdController _adController;

    #endregion

    private void OnEnable()
    {
        if (_adController == null)
        {
            _adController = FindAnyObjectByType<InterstitialAdController>();
        }
    }

    void Start()
    {
        LoadInterstitialAd();
    }

    public void LoadInterstitialAd()
    {
        if (PlayerPrefs.GetInt("adsRemoved") == 0)
        {
                _adController.LoadAd();
        }
    }
    public void ShowInterstitialAd()
    {
        _adController.ShowAd();

        FirebaseAnalytics.LogEvent(name: "interstitial_ad_showed");
    }
}
