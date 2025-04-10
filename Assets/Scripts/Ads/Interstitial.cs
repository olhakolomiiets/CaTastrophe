using Firebase.Analytics;
using GoogleMobileAds.Sample;
using UnityEngine;

public class Interstitial : MonoBehaviour
{

    #region EDITOR FIELDS

    private int delayBetweenAds = 120;
    [SerializeField] private InterstitialAdController _adController;

    #endregion

    #region PRIVATE FIELDS

    private static float lastAdTime = Mathf.NegativeInfinity;

    #endregion

    private void OnEnable()
    {
        if (_adController == null)
        {
            _adController = FindAnyObjectByType<InterstitialAdController>();
        }

        PlayerPrefs.SetInt("HowManyGamesPlayed", PlayerPrefs.GetInt("HowManyGamesPlayed") + 1);

        var _playCount = PlayerPrefs.GetInt("HowManyGamesPlayed");

        FirebaseAnalytics.LogEvent(name: "games_count");
    }

    void Start()
    {
        LoadInterstitialAd();
    }

    public void LoadInterstitialAd()
    {
        if(PlayerPrefs.GetInt("HowManyGamesPlayed") < 1)
        {
            return;
        }

        if (PlayerPrefs.GetInt("adsRemoved") == 0)
        {
            if ((Time.time - lastAdTime) > (float)delayBetweenAds)
            {
                _adController.LoadAd();

                lastAdTime = Time.time;
                Debug.Log("Show Interstitial With Delay Between Ads " + lastAdTime);
            }
        }
    }
    public void ShowInterstitialAd()
    {
        if (PlayerPrefs.GetInt("HowManyGamesPlayed") < 3)
        {
            return;
        }
        _adController.ShowAd();

        FirebaseAnalytics.LogEvent(name: "interstitial_ad_showed");
    }
}
