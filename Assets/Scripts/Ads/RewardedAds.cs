// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using GoogleMobileAds.Api;
// using System;
// public class RewardedAds : MonoBehaviour
// {
//     public RewardBasedVideoAd rewardBasedVideoAd;
//     private const string rewardedUnitId = "ca-app-pub-3940256099942544/5224354917";
//     // private void OnEnable() {
//     //     rewardBasedVideoAd = new RewardedAd(rewardedUnitId);
//     //     AdRequest adRequest = new AdRequest.Builder().Build();
//     //     rewardBasedVideoAd.LoadAd(adRequest);

//     // }

//     public void Start()
//     {
//         rewardBasedVideoAd = RewardBasedVideoAd.Instance;

//         rewardBasedVideoAd.OnAdClosed += HandleOnAdClosed;
//         rewardBasedVideoAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
//         rewardBasedVideoAd.OnAdLeavingApplication += HandleOnAdLeavingApplication;
//         rewardBasedVideoAd.OnAdLoaded += HandleOnAdLoaded;
//         rewardBasedVideoAd.OnAdOpening += HandleOnAdOpening;
//         rewardBasedVideoAd.OnAdRewarded += HandleOnAdRewarded;
//         rewardBasedVideoAd.OnAdStarted += HandleOnAdStarted;

//     }

//     public void ShowAd()
//     {
//         if (rewardBasedVideoAd.IsLoaded())
//         {
//             rewardBasedVideoAd.Show();
//         }
//     }

//     public void LoadRewardAd()
//     {
// #if UNITY_EDITOR
//         string adUnitId = "unused";
// #elif UNITY_ANDROID
//         string rewardedUnitId = "ca-app-pub-3940256099942544/5224354917";
// #else
//         string rewardedUnitId = "unexpectPlatform";
// #endif
//         rewardBasedVideoAd.LoadAd(new AdRequest.Builder().Build(), adUnitId);

//     }

//     public void ShowRewardAd()
//     {
//         if (rewardBasedVideoAd.IsLoaded())
//         {
//             rewardBasedVideoAd.Show();
//         }
//         else
//         {
//             MonoBehaviour.print("No Fucking Ads");
//         }
//     }


//     public void HandleOnAdLoaded(object sender, EventArgs args)
//     {

//     }

//     public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//     {

//     }

//     public void HandleOnAdOpening(object sender, EventArgs args)
//     {

//     }

//     public void HandleOnAdStarted(object sender, EventArgs args)
//     {

//     }

//     public void HandleOnAdClosed(object sender, EventArgs args)
//     {

//     }

//     public void HandleOnAdRewarded(object sender, Reward args)
//     {
//         MonoBehaviour.print(string.Format("You just got {0} {1}!", args.Amount, args.Type));
//     }

//     public void HandleOnAdLeavingApplication(object sender, EventArgs args)
//     {

//     }



// }
