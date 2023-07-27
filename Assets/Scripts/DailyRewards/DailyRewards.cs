using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewards : MonoBehaviour
{
    [SerializeField] private Text status;
    [SerializeField] private Button claimButton;

    [Space(5)]
    [SerializeField] private DailyRewardPref rewardPref;
    [SerializeField] private Transform rewardsGrid;

    [Space(5)]
    [SerializeField] private List<DailyReward> rewards;
    [SerializeField] private List<DailyRewardPref> rewardPrefabs;

    private int currentStreak
    {
        get => PlayerPrefs.GetInt("currentStreak", 0);
        set => PlayerPrefs.SetInt("currentStreak", value);
    }

    private DateTime? lastClaimTime
    {
        get
        {
            string data = PlayerPrefs.GetString("lastClamedTime", null);

            if (!string.IsNullOrEmpty(data))
                return DateTime.Parse(data);

            return null;
        }
        set
        {
            if(value != null)
                PlayerPrefs.SetString("lastClamedTime", value.ToString());
            else
                PlayerPrefs.DeleteKey("lastClamedTime");
        }
    }

    private bool canClaimReward;
    private int maxStreakCount = 10;
    private float claimCooldown = 24f / 24 / 60 / 2;
    private float claimDeadline = 48f / 24 / 60 / 2;

    private bool receivedReward;


    private void Start()
    {
        InitPrefabs();
        StartCoroutine(RewardsStateUpdater());
    }

    private void InitPrefabs()
    {
        rewardPrefabs = new List<DailyRewardPref>();

        for (int i = 0; i < maxStreakCount; i++)
            rewardPrefabs.Add(Instantiate(rewardPref, rewardsGrid, false));

    }

    private IEnumerator RewardsStateUpdater()
    {
        while (true)
        {
            UpdateRewardsState();
            yield return new WaitForSeconds(1);
        }
    }

    private void UpdateRewardsState()
    {
        canClaimReward = true;

        if (lastClaimTime.HasValue)
        {
            var timeSpan = DateTime.UtcNow - lastClaimTime.Value;

            if (timeSpan.TotalHours > claimDeadline)
            {
                lastClaimTime = null;
                currentStreak = 0;
            }
            else if (timeSpan.TotalHours < claimCooldown)
                canClaimReward = false;
        }
        UpdateRewardUI();
    }

    private void UpdateRewardUI()
    {
        claimButton.interactable = canClaimReward;

        if (canClaimReward)
        {
            status.text = $"{Lean.Localization.LeanLocalization.GetTranslationText("claimYourReward")}";
        }
        else
        {
            var nextClaimTime = lastClaimTime.Value.AddHours(claimCooldown);
            var currentClaimCooldown = nextClaimTime - DateTime.UtcNow;

            string cd = $"{currentClaimCooldown.Hours:D2}:{currentClaimCooldown.Minutes:D2}:{currentClaimCooldown.Seconds:D2}";

            status.text = $"{Lean.Localization.LeanLocalization.GetTranslationText("dailyRewardStatus1")} {cd} {Lean.Localization.LeanLocalization.GetTranslationText("dailyRewardStatus2")}";
        }

        for (int i = 0; i < rewardPrefabs.Count; i++)
        {
            rewardPrefabs[i].SetRewardData(i, currentStreak, rewards[i]);

            rewardPrefabs[i].UpdateUI(i, currentStreak);
        }           
    }

    public void ClaimReward()
    {
        if (!canClaimReward)
            return;

        var reward = rewards[currentStreak];
        switch (reward.Type)
        {
            case DailyReward.RewardType.COINS:
                GameController.Instance.AddCoins(reward.Value);
                break;
            case DailyReward.RewardType.SAND:
                GameController.Instance.AddSand(reward.Value);
                break;
            case DailyReward.RewardType.FOOD:
                GameController.Instance.AddFood(reward.Value);
                break;
            case DailyReward.RewardType.ENERGY_RECOVERY:
                GameController.Instance.AddEnergy(reward.Value);
                break;
        }

        lastClaimTime = DateTime.UtcNow;
        currentStreak = (currentStreak + 1) % maxStreakCount;

        UpdateRewardsState();
    }


}
