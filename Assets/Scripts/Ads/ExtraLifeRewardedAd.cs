using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ExtraLifeRewardedAd : MonoBehaviour
{
    private RewardBasedVideoAd adReward;
    private string idApp, idReward;
    [SerializeField] private Button buttonReward;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject panelLose;
    private bool _rewardedAdUsed;

    private void OnEnable()
    {
        _rewardedAdUsed = false;
        Debug.Log("OnEnable ()");
    }

    void Start()
    {
        //idApp = "ca-app-pub-4196164004824768~8007119707";
        idApp = "ca-app-pub-3940256099942544/5224354917";
        idReward = "ca-app-pub-3940256099942544/5224354917";

        adReward = RewardBasedVideoAd.Instance;
    }

    public void RequestRewardAd()
    {
        AdRequest request = AdRequestBuild();
        adReward.LoadAd(request, idReward);

        adReward.OnAdLoaded += this.HandleOnRewardedAdLoaded;
        adReward.OnAdRewarded += this.UserEarnedReward;
        adReward.OnAdClosed += this.RewardedAdClosed;
    }

    AdRequest AdRequestBuild()
    {
        return new AdRequest.Builder().Build();
    }

    public void ShowRewardAd()
    {
        if (adReward.IsLoaded())
            adReward.Show();
        Debug.Log("adReward.Show ()");
    }

    public void HandleOnRewardedAdLoaded(object sender, EventArgs args)
    {
        ShowRewardAd();
    }
    public void UserEarnedReward (object sender, EventArgs args)
    {
        CowController.lives++;
        Debug.Log("User Earned Reward ()");
    }
    public void RewardedAdClosed (object sender, EventArgs args)
    {
        buttonReward.interactable = true;
        buttonReward.GetComponentInChildren<Text>().text = $"{Lean.Localization.LeanLocalization.GetTranslationText("GetLife")}";
        Time.timeScale = 1;
        panelLose.SetActive(false);
        pauseButton.GetComponent<Button>().interactable = true;
        _rewardedAdUsed = true;
        Debug.Log("Rewarded Ad Closed ()");
    }
    public void OnGetOneMoreLife()
    {
        buttonReward.interactable = false;
        buttonReward.GetComponentInChildren<Text>().text = $"{Lean.Localization.LeanLocalization.GetTranslationText("loading")}";
        RequestRewardAd();
    }

    private void OnDisable()
    {
        adReward.OnAdLoaded -= this.HandleOnRewardedAdLoaded;
        adReward.OnAdRewarded -= this.UserEarnedReward;
        adReward.OnAdClosed -= this.RewardedAdClosed;
    }

}
