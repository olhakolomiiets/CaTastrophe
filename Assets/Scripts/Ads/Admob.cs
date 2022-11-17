// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using GoogleMobileAds.Api;
// using UnityEngine.UI;

    
// }

// public class Admob : MonoBehaviour
// {
//     public delegate void EventPrototype(System.EventArgs args);
//     private BannerView adBanner;
//     private InterstitialAd adInterstitial;
//     private RewardBasedVideoAd adReward;

//     private string idApp, idBanner, idInterstitial, idReward;
//     [SerializeField] Button BtnInterstitial;
//     [SerializeField] Button BtnReward;
//     void Start()
//     {
//         BtnInterstitial.interactable = false;
//         idApp = "ca-app-pub-4196164004824768~8007119707";
//         idBanner = "ca-app-pub-3940256099942544/6300978111";
//         idInterstitial = "ca-app-pub-3940256099942544/8691691433";
//         idReward = "ca-app-pub-3940256099942544/5224354917";

//         adReward = RewardBasedVideoAd.Instance;

//         MobileAds.Initialize(idApp);
//         RequestBannerAd();
//         RequestInterstitialAd;
//     }

// public




//     public void RequestRewardAd()
//     {
//         AdRequest request = AdRequestBuild();
//         adReward.LoadAd(request, idReward);

//         adReward.OnAdLoaded += this.HandleOnRewardedAdLoaded;
//         adReward.OnAdRewarded += this.HandleOnRewardedAdOpening;
//         adReward.OnAdRewarded += this.HandleOnAdRewarded;
//         adReward.OnAdClosed += this.HandleOnRewardedAdClosed;
//     }

//     public void ShowRewardAd()
//     {
//         if (adReward.IsLoaded())
//             adReward.Show();
//     }

//     public void HandleOnRewardedAdLoaded (object sender, Reward args) {

//     }
//     public void HandleOnRewardedAdOpening (object sender, Reward args) {

//     }
//     public void HandleOnAdRewarded (object sender, Reward args) {

//     }
//     public void HandleOnRewardedAdClosed (object sender, Reward args) {

//     }



//     void Update()
//     {

//     }
// }
