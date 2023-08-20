using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Firebase.Analytics;
using System.Collections;

public class ExtraLifeRewardedAd : MonoBehaviour
{
    #region EDITOR FIELDS

    [SerializeField] private Button buttonReward;
    [SerializeField] private GameObject panelLose;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GoogleMobileAds.Sample.RewardedAdController _adController;

    #endregion

    #region UNITY EVENTS

    [HideInInspector] public UnityEvent OnUserEarnedRewardEvent;
    [HideInInspector] public UnityEvent OnAdClosedEvent;
    [HideInInspector] public UnityEvent RewardedAdLoadedEvent;
    [HideInInspector] public UnityEvent RewardedAdLoadedWithErrorEvent;

    #endregion

    #region PRIVATE FIELDS

    private bool _rewardedAdUsed;

    #endregion


    private void OnEnable()
    {
        _adController.OnUserEarnedRewardEvent.AddListener(UserEarnedReward);
        _adController.OnAdClosedEvent.AddListener(RewardedAdClosed);
        _adController.RewardedAdLoadedEvent.AddListener(ShowRewardedAd);
        _adController.RewardedAdLoadedWithErrorEvent.AddListener(RewardedAdWithError);

        if (!_rewardedAdUsed)
        {            
            buttonReward.interactable = true;
        }
        else
        {
            buttonReward.interactable = false;
        }
    }

    public void UserEarnedReward()
    {
        CowController.lives++;
    }

    public void RewardedAdClosed()
    {
        buttonReward.GetComponentInChildren<Text>().text = $"{Lean.Localization.LeanLocalization.GetTranslationText("GetLife")}";
        Time.timeScale = 1;
        panelLose.SetActive(false);
        pauseButton.GetComponent<Button>().interactable = true;
        _rewardedAdUsed = true;

        FirebaseAnalytics.LogEvent(name: "got_life_for_ads");
    }


    public void OnGetOneMoreLife()
    {
        buttonReward.interactable = false;
        buttonReward.GetComponentInChildren<Text>().text = $"{Lean.Localization.LeanLocalization.GetTranslationText("loading")}";

        _adController.LoadAd();
    }

    public void ShowRewardedAd()
    {
        _adController.ShowAd();
    }

    public void RewardedAdWithError()
    {
        buttonReward.GetComponentInChildren<Text>().text = $"{Lean.Localization.LeanLocalization.GetTranslationText("rewardedAdError")}";
    }

    private void OnDisable()
    {
        _adController.OnUserEarnedRewardEvent.RemoveListener(UserEarnedReward);
        _adController.OnAdClosedEvent.RemoveListener(RewardedAdClosed);
        _adController.RewardedAdLoadedEvent.RemoveListener(ShowRewardedAd);
        _adController.RewardedAdLoadedWithErrorEvent.RemoveListener(RewardedAdWithError);
    }


}
