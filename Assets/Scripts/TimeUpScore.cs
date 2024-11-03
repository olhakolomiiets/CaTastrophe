using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.Events;
using Firebase.Analytics;
using DG.Tweening;

public class TimeUpScore : MonoBehaviour
{
    #region EDITOR FIELDS
    public Text scoreTimeUpText;
    public Text scoreTotalText;
    [SerializeField] private Text extraCoinsText;
    [SerializeField] private Button buttonReward;
    [SerializeField] private Text serviceText;
    [SerializeField] private GameObject rewardMsg;
    [SerializeField] private Text rewardText;
    [SerializeField] private GoogleMobileAds.Sample.RewardedAdController _adController;
    #endregion

    #region UNITY EVENTS
    [HideInInspector] public UnityEvent OnUserEarnedRewardEvent;
    [HideInInspector] public UnityEvent RewardedAdLoadedEvent;
    [HideInInspector] public UnityEvent RewardedAdLoadedWithErrorEvent;
    #endregion

    #region PRIVATE FIELDS
    private ScoreManager sm;
    private int score;
    private int totalScore;
    private int extraCoins;
    private int tScore;
    private float animationDuration = 0.5f;
    private Sequence loadingSequence;

    #endregion

    private void OnEnable()
    {
        _adController.OnUserEarnedRewardEvent.AddListener(UserEarnedReward);
        _adController.RewardedAdLoadedEvent.AddListener(ShowRewardedAd);
        _adController.RewardedAdLoadedWithErrorEvent.AddListener(RewardedAdWithError);

        sm = FindObjectOfType<ScoreManager>();
        totalScore = sm.TotalScore;
        if (sm.score > PlayerPrefs.GetInt("AwardMoneyPerHouse", 0))
        {
            PlayerPrefs.SetInt("AwardMoneyPerHouse", sm.score);
        }
        StartCoroutine("Counter");
        StartCoroutine("CounterTotal");
        StartCoroutine("ExtraCoinsCounter");
    }

    IEnumerator Counter()
    {
        for (int i = 1; i <= sm.score; i += 4)
        {
            scoreTimeUpText.text = i.ToString();

            yield return null;
        }
        scoreTimeUpText.text = sm.score.ToString();

    }

    IEnumerator ExtraCoinsCounter()
    {
        extraCoins = sm.score / 2;

        for (int i = 1; i <= extraCoins; i += 4)
        {
            extraCoinsText.text = i.ToString();

            yield return null;
        }
        extraCoinsText.text = extraCoins.ToString();
    }

    IEnumerator CounterTotal()
    {
        if (sm.TotalScore > PlayerPrefs.GetInt("AwardTotalMoney"))
        {
            PlayerPrefs.SetInt("AwardTotalMoney", sm.TotalScore);
        }
        for (int i = totalScore - sm.score; i <= sm.TotalScore; i += 4)
        {
            scoreTotalText.text = i.ToString();

            yield return null;
        }
        scoreTotalText.text = sm.TotalScore.ToString();
    }

    public void UserEarnedReward()
    {
        tScore = PlayerPrefs.GetInt("TotalScore");
        tScore = tScore + extraCoins;
        SoundManager.snd.PlaybuySounds();
        PlayerPrefs.SetInt("TotalScore", tScore);
        serviceText.gameObject.SetActive(false);

        scoreTotalText.text = tScore.ToString();
        rewardMsg.SetActive(true);
        rewardText.text = extraCoins.ToString();

        FirebaseAnalytics.LogEvent(name: "got_extraCoins_for_ads");
    }

    public void GetExtraCoins()
    {
        buttonReward.gameObject.SetActive(false);
        serviceText.gameObject.SetActive(true);
        serviceText.text = $"{Lean.Localization.LeanLocalization.GetTranslationText("loading")}";    

        _adController.LoadAd();
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
        StopCoroutine("Counter");
        StopCoroutine("CounterTotal");
        StopCoroutine("ExtraCoinsCounter");

        _adController.OnUserEarnedRewardEvent.RemoveListener(UserEarnedReward);
        _adController.RewardedAdLoadedEvent.RemoveListener(ShowRewardedAd);
        _adController.RewardedAdLoadedWithErrorEvent.RemoveListener(RewardedAdWithError);
    }
}