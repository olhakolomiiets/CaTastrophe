using GoogleMobileAds.Sample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerAd : MonoBehaviour
{
    [SerializeField] private BannerViewController _adController;
    //[SerializeField] private AppodealAdController _appodealController;

    private void OnEnable()
    {
        if (_adController == null)
        {
            _adController = FindAnyObjectByType<BannerViewController>();
        }
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("adsRemoved") == 0)
        {
            //_appodealController.ShowBannerBottom();
            _adController.LoadAd();
        }
    }

    private void OnDisable()
    {
        //_appodealController.HideBanner();
        _adController.HideAd();
    }
}
