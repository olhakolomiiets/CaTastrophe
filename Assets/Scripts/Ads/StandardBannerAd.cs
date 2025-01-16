using GoogleMobileAds.Sample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBannerAd : MonoBehaviour
{
    [SerializeField] private StandardBannerViewController _adController;
    //[SerializeField] private AppodealAdController _appodealController;

    private void OnEnable()
    {
        if (_adController == null)
        {
            _adController = FindAnyObjectByType<StandardBannerViewController>();
        }
        if (PlayerPrefs.GetInt("adsRemoved") == 0)
        {
            _adController.LoadAd();
            //_appodealController.ShowBannerBottom();
        }
    }

    private void OnDisable()
    {
        _adController.HideAd();
        //_appodealController.HideBanner();
    }
}
