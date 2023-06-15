using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interstitial : MonoBehaviour
{

    #region EDITOR FIELDS

    [SerializeField, Range(1, 900)] private int delayBetweenAds = 240;
    [SerializeField] private GoogleMobileAds.Sample.InterstitialAdController _adController;

    #endregion

    #region PRIVATE FIELDS

    private static float lastAdTime = Mathf.NegativeInfinity;

    #endregion

    void Start()
    {
        LoadInterstitialAd();
    }

    public void LoadInterstitialAd()
    {
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
        _adController.ShowAd();
    }
}
