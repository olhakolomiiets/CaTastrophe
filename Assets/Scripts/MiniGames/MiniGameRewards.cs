using Firebase.Analytics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameRewards : MonoBehaviour
{
    [SerializeField] private int levelIndex;

    [Space(5)]
    [SerializeField] private MiniGameRewardPref rewardPref;
    [SerializeField] private Transform rewardsGrid;

    [Space(5)]
    [SerializeField] private List<MiniGameReward> rewards;
    [SerializeField] private List<MiniGameRewardPref> rewardPrefabs;

    private int _star1;
    private int _star2;
    private int _star3;

    private string _levelStar1;
    private string _levelStar2;
    private string _levelStar3;

    private int[] activeRewards;
    private string[] levelStars;

    private int maxRewardsCount = 3;

    private void Start()
    {
        _levelStar1 = "LevelStar1" + levelIndex;
        _levelStar2 = "LevelStar2" + levelIndex;
        _levelStar3 = "LevelStar3" + levelIndex;

        _star1 = PlayerPrefs.GetInt(_levelStar1);
        _star2 = PlayerPrefs.GetInt(_levelStar2);
        _star3 = PlayerPrefs.GetInt(_levelStar3);

        activeRewards = new int[] { _star1, _star2, _star3 };

        levelStars = new string[] { _levelStar1, _levelStar2, _levelStar3 };

        InitPrefabs();
        UpdateRewardUI();
    }

    private void InitPrefabs()
    {
        rewardPrefabs = new List<MiniGameRewardPref>();

        for (int i = 0; i < maxRewardsCount; i++)
            rewardPrefabs.Add(Instantiate(rewardPref, rewardsGrid, false));
    }

    private void UpdateRewardUI()
    {
        for (int i = 0; i < rewardPrefabs.Count; i++)
        {
            rewardPrefabs[i].SetRewardData(activeRewards[i], rewards[i], levelStars[i]);
        }
    }
}
