using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Firebase.Analytics;
using System.Collections;
using GoogleMobileAds.Sample;

public class ExtraLifeRewardedAd : MonoBehaviour
{
    #region EDITOR FIELDS

    [SerializeField] private Button buttonReward;
    [SerializeField] private GameObject panelLose;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private RewardedAdController _adController;
    [SerializeField] private Text serviceText;
    [SerializeField] private GameObject description;
    [SerializeField] private GameObject sadCatHead;

    //[SerializeField] private AppodealAdController _appodealController;

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
        if (_adController == null)
        {
            _adController = FindAnyObjectByType<RewardedAdController>();
        }
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
        FirebaseAnalytics.LogEvent(name: "got_life_for_ads");
    }

    public void RewardedAdClosed()
    {
        panelLose.SetActive(false);
        sadCatHead.SetActive(true);
        serviceText.gameObject.SetActive(false);
        description.SetActive(false);
        pauseButton.GetComponent<Button>().interactable = true;
        Time.timeScale = 1;
        _rewardedAdUsed = true;       
    }


    public void OnGetOneMoreLife()
    {
        buttonReward.gameObject.SetActive(false);
        serviceText.gameObject.SetActive(true);
        serviceText.text = $"{Lean.Localization.LeanLocalization.GetTranslationText("loading")}";

        _adController.LoadAd();
        //_appodealController.ShowRewardedVideo();
    }

    public void ShowRewardedAd()
    {
        _adController.ShowAd();
    }

    public void RewardedAdWithError()
    {
        serviceText.text = $"{Lean.Localization.LeanLocalization.GetTranslationText("rewardedAdError")}";
    }

    private void OnDisable()
    {
        _adController.OnUserEarnedRewardEvent.RemoveListener(UserEarnedReward);
        _adController.OnAdClosedEvent.RemoveListener(RewardedAdClosed);
        _adController.RewardedAdLoadedEvent.RemoveListener(ShowRewardedAd);
        _adController.RewardedAdLoadedWithErrorEvent.RemoveListener(RewardedAdWithError);
    }


}
