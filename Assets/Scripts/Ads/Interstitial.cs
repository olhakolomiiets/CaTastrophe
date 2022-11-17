using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;
//Interstitial ad

public class Interstitial : MonoBehaviour
{
    private InterstitialAd adInterstitial;

    private string idApp, idInterstitial;

    // [SerializeField] Button BtnInterstitial;
	   static int counter = 0;


    void Start()
    {
        // BtnInterstitial.interactable = false;

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

        //attach events
        adInterstitial.OnAdLoaded += this.HandleOnAdLoaded;
        adInterstitial.OnAdOpening += this.HandleOnAdOpening;
        adInterstitial.OnAdClosed += this.HandleOnAdClosed;
    }

    public void ShowInterstitialAd()
    {
        if (adInterstitial.IsLoaded())
            adInterstitial.Show();
    }

    public void DestroyInterstitialAd()
    {
        adInterstitial.Destroy();
    }

    //interstitial ad events
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        //this method executes when interstitial ad is Loaded and ready to show
        // BtnInterstitial.interactable = true; //button is ready to click (enabled)
    }

    public void HandleOnAdOpening(object sender, EventArgs args)
    {
        //this method executes when interstitial ad is shown
        // BtnInterstitial.interactable = false; //disable the button
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        //this method executes when interstitial ad is closed
        adInterstitial.OnAdLoaded -= this.HandleOnAdLoaded;
        adInterstitial.OnAdOpening -= this.HandleOnAdOpening;
        adInterstitial.OnAdClosed -= this.HandleOnAdClosed;

        RequestInterstitialAd(); //request new interstitial ad after close
    }

    #endregion


    //------------------------------------------------------------------------
    AdRequest AdRequestBuild()
    {
        return new AdRequest.Builder().Build();
    }

    void OnDestroy()
    {
        DestroyInterstitialAd();

        //dettach events
        adInterstitial.OnAdLoaded -= this.HandleOnAdLoaded;
        adInterstitial.OnAdOpening -= this.HandleOnAdOpening;
        adInterstitial.OnAdClosed -= this.HandleOnAdClosed;
    }

 


    void ShowAd()
    {
        counter++;
        if (counter == 3)
        {
            counter = 0;
            ShowInterstitialAd();
        }
    }

}