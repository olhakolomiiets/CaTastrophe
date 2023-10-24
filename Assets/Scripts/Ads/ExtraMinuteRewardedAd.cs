using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Firebase.Analytics;
using System.Collections;

public class ExtraMinuteRewardedAd : MonoBehaviour
{
    #region EDITOR FIELDS

    [SerializeField] private Button buttonReward;
    [SerializeField] private GameObject panelGetTime;
    [SerializeField] private GoogleMobileAds.Sample.RewardedAdController _adController;
    [SerializeField] private GameTimer gameTimer;
    [SerializeField] private int extraTime;
    [SerializeField] private GameObject pauseButton;

    #endregion

    #region UNITY EVENTS

    [HideInInspector] public UnityEvent OnUserEarnedRewardEvent;
    [HideInInspector] public UnityEvent OnAdClosedEvent;
    [HideInInspector] public UnityEvent RewardedAdLoadedEvent;
    [HideInInspector] public UnityEvent RewardedAdLoadedWithErrorEvent;

    #endregion

    #region PRIVATE FIELDS

    private bool _rewardedAdUsed;

    private CowController player;
    private Rigidbody2D rbPlayer;

    #endregion

    private void Awake()
    {
        player = FindObjectOfType<CowController>();
        rbPlayer = player.transform.GetComponentInParent<Rigidbody2D>();
    }
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
        gameTimer.AddSecondsToTimer(extraTime);
        FirebaseAnalytics.LogEvent(name: "got_minute_for_ads");
    }

    public void RewardedAdClosed()
    {        
        Time.timeScale = 1;
        //rbPlayer.isKinematic = false;
        //rbPlayer.constraints = RigidbodyConstraints2D.None;
        panelGetTime.SetActive(false);
        pauseButton.GetComponent<Button>().interactable = true;
        _rewardedAdUsed = true;
        gameTimer.TimeUp = false;
    }


    public void OnGetOneMinute()
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
