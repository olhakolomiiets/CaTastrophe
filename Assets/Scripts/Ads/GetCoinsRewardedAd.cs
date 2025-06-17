using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Firebase.Analytics;
using GoogleMobileAds.Sample;

public class GetCoinsRewardedAd : MonoBehaviour
{
    #region EDITOR FIELDS
    [Header("Coins")]
    [SerializeField] private Button buttonReward;
    [SerializeField] private GameObject getCoinsForAdsWindow;

    [SerializeField] private int dailyBonus;
    [SerializeField] AdBonusTimer bonusTimer;

    [Header("Energy Recovery")]
    [SerializeField] private GameObject getEnergyButton;
    [SerializeField] private Button buttonEnergyReward;
    [SerializeField] private GameObject getEnergyForAdsWindow;
    [SerializeField] private PowersRestore energyRecovery;
    [SerializeField] private int amountPowersToRestore;

    [Space(5)]
    [SerializeField] private RewardedAdController _adController;

    #endregion

    #region UNITY EVENTS

    [HideInInspector] public UnityEvent OnUserEarnedRewardEvent;
    [HideInInspector] public UnityEvent RewardedAdLoadedEvent;
    [HideInInspector] public UnityEvent RewardedAdLoadedWithErrorEvent;

    #endregion

    #region PRIVATE FIELDS

    private bool _rewardedAdUsed;
    private int TotalScore;
    private ScoreManager sm;
    private bool isCoins;
    private bool isEnergy;

    #endregion

    private void OnEnable()
    {
        if (_adController == null)
        {
            _adController = FindAnyObjectByType<RewardedAdController>();
        }

        sm = FindAnyObjectByType<ScoreManager>();

        _adController.OnUserEarnedRewardEvent.AddListener(UserEarnedReward);
        _adController.RewardedAdLoadedEvent.AddListener(ShowRewardedAd);
        _adController.RewardedAdLoadedWithErrorEvent.AddListener(RewardedAdWithError);
    }

    public void UserEarnedReward()
    {

        if (isCoins)
        {
            isCoins = false;
            TotalScore = PlayerPrefs.GetInt("TotalScore");
            TotalScore = TotalScore + dailyBonus;

            sm.UpdateAwardTotalScore(dailyBonus);
            SoundManager.snd.PlaybuySounds();
            PlayerPrefs.SetInt("TotalScore", TotalScore);
            bonusTimer.AdViewed();

            FirebaseAnalytics.LogEvent(name: "got_250coins_for_ads");

            buttonReward.interactable = true;
            getCoinsForAdsWindow.SetActive(false);
        }
        else if (isEnergy)
        {
            isEnergy = false;
            getEnergyButton.SetActive(false);
            int powersToRestore = PlayerPrefs.GetInt("countPowersToRestore");
            powersToRestore = powersToRestore + amountPowersToRestore;
            PlayerPrefs.SetInt("countPowersToRestore", powersToRestore);
            SoundManager.snd.PlaybuySounds();

            buttonEnergyReward.interactable = true;
            getEnergyForAdsWindow.SetActive(false);

            energyRecovery.UpdateUI();
        }

    }

    public void GetCoins()
    {
        isCoins = true;
        buttonReward.interactable = false;
        buttonReward.GetComponentInChildren<Text>().text = $"{Lean.Localization.LeanLocalization.GetTranslationText("loading")}";

        _adController.LoadAd();
    }

    public void GetEnergy()
    {
        isEnergy = true;
        buttonEnergyReward.interactable = false;
        buttonEnergyReward.GetComponentInChildren<Text>().text = $"{Lean.Localization.LeanLocalization.GetTranslationText("loading")}";

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
        _adController.RewardedAdLoadedEvent.RemoveListener(ShowRewardedAd);
        _adController.RewardedAdLoadedWithErrorEvent.RemoveListener(RewardedAdWithError);
    }
}