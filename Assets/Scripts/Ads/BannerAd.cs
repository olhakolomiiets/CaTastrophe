using GoogleMobileAds.Sample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerAd : MonoBehaviour
{
    [SerializeField] private BannerViewController _adController;

    private void OnEnable()
    {
        if (_adController == null)
        {
            _adController = FindAnyObjectByType<BannerViewController>();
        }

        if (PlayerPrefs.GetInt("adsRemoved") == 0)
        {
            _adController.LoadAd();
        }
    }

    private void OnDisable()
    {
        _adController.HideAd();
    }
}
