using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;

public class Interstitial : MonoBehaviour
{
    private InterstitialAd adInterstitial;
    private string idApp, idInterstitial;
/*	static int counter = 0; */
    [SerializeField, Range(1, 900)] private int delayBetweenAds = 240;
    private static float lastAdTime = Mathf.NegativeInfinity;


    void Start()
    {
        idApp = "ca-app-pub-4196164004824768~8007119707";
        idInterstitial = "ca-app-pub-3940256099942544/8691691433";

        MobileAds.Initialize(idApp);
       
            RequestInterstitialAd();
    }

    #region Interstitial methods ---------------------------------------------

    public void RequestInterstitialAd()
    {
        adInterstitial = new InterstitialAd(idInterstitial);
        AdRequest request = AdRequestBuild();
        adInterstitial.LoadAd(request);
        
        adInterstitial.OnAdLoaded += this.HandleOnAdLoaded;
        adInterstitial.OnAdOpening += this.HandleOnAdOpening;
        adInterstitial.OnAdClosed += this.HandleOnAdClosed;
    }

    public void ShowInterstitialAd()
    {
        if (adInterstitial.IsLoaded())
            adInterstitial.Show();
    }

    public void ShowInterstitialWithDelayBetweenAds()
    {
        if ((Time.time - lastAdTime) > (float)delayBetweenAds) 
        {
            if (adInterstitial.IsLoaded())
            {
                adInterstitial.Show();
                lastAdTime = Time.time;
                Debug.Log("ShowInterstitialWithDelayBetweenAds " + lastAdTime);
            }
        }
    }

    public void DestroyInterstitialAd()
    {
        adInterstitial.Destroy();
    }
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
    }
    public void HandleOnAdOpening(object sender, EventArgs args)
    {
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {

        adInterstitial.OnAdLoaded -= this.HandleOnAdLoaded;
        adInterstitial.OnAdOpening -= this.HandleOnAdOpening;
        adInterstitial.OnAdClosed -= this.HandleOnAdClosed;

        RequestInterstitialAd();
    }

    #endregion

    AdRequest AdRequestBuild()
    {
        return new AdRequest.Builder().Build();
    }

    void OnDestroy()
    {
        DestroyInterstitialAd();

        adInterstitial.OnAdLoaded -= this.HandleOnAdLoaded;
        adInterstitial.OnAdOpening -= this.HandleOnAdOpening;
        adInterstitial.OnAdClosed -= this.HandleOnAdClosed;
    }

/*    void ShowAd()
    {
        if (PlayerPrefs.GetInt("adsRemoved") == 0)
        {
            counter++;
            if (counter == 3)
            {
                counter = 0;
                ShowInterstitialAd();
            }
        }
    }*/

}