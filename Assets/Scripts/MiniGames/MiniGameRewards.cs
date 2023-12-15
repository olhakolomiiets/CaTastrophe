using Firebase.Analytics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameRewards : MonoBehaviour
{
    [SerializeField] private MiniGamesSO miniGameStars;

    [Space(5)]
    [SerializeField] private MiniGameRewardPref rewardPref;
    [SerializeField] private MiniGameRewardPref rewardPrefStreet;
    [SerializeField] private Transform rewardsGrid;

    [Space(5)]
    [SerializeField] private List<MiniGameReward> rewards;
    [SerializeField] private List<MiniGameRewardPref> rewardPrefabs;

    [SerializeField] private GameObject sadCatHead;
    [SerializeField] private GameObject happyCatHead;

    [SerializeField] private bool isStreet;

    private int _star1;
    private int _star2;
    private int _star3;

    private string _levelStar1;
    private string _levelStar2;
    private string _levelStar3;

    private int[] activeRewards;
    private string[] levelStars;

    private int maxRewardsCount = 3;

    private bool[] isRewardsShow;

    private void Start()
    {
        _levelStar1 = "LevelStar1" + miniGameStars.LevelIndex;
        _levelStar2 = "LevelStar2" + miniGameStars.LevelIndex;
        _levelStar3 = "LevelStar3" + miniGameStars.LevelIndex;

        _star1 = PlayerPrefs.GetInt(_levelStar1);
        _star2 = PlayerPrefs.GetInt(_levelStar2);
        _star3 = PlayerPrefs.GetInt(_levelStar3);

        Debug.Log("Level Star 1 " + PlayerPrefs.GetInt(_levelStar1));
        Debug.Log("Level Star 2 " + PlayerPrefs.GetInt(_levelStar2));
        Debug.Log("Level Star 3 " + PlayerPrefs.GetInt(_levelStar3));

        Debug.Log("is Rewards Show 1" + miniGameStars.IsReward1Show);
        Debug.Log("is Rewards Show 2" + miniGameStars.IsReward2Show);
        Debug.Log("is Rewards Show 3" + miniGameStars.IsReward3Show);

        levelStars = new string[] { _levelStar1, _levelStar2, _levelStar3 };
        activeRewards = new int[] { _star1, _star2, _star3 };
        isRewardsShow = new bool[] { miniGameStars.IsReward1Show, miniGameStars.IsReward2Show, miniGameStars.IsReward3Show };

        InitPrefabs();       
        InitCatPrefab();
    }

    private void InitPrefabs()
    {
        if (!isStreet)
            StartCoroutine(InitPrefabsRoutine());                      
        else InitPrefabsForStreet();
    }

    private void InitPrefabsForStreet()
    {
        rewardPrefabs = new List<MiniGameRewardPref>();

        for (int i = 0; i < maxRewardsCount; i++)
        {
            rewardPrefabs.Add(Instantiate(rewardPrefStreet, rewardsGrid, false));
        }

        UpdateRewardUI();
    }
    
    private void UpdateRewardUI()
    {
        for (int i = 0; i < rewardPrefabs.Count; i++)
        {
            rewardPrefabs[i].SetRewardDataForStreet(activeRewards[i], rewards[i], levelStars[i]);
        }
    }          

    IEnumerator InitPrefabsRoutine()
    {
        rewardPrefabs = new List<MiniGameRewardPref>();

        for (int i = 0; i < maxRewardsCount; i++)            
        {
            if (isRewardsShow[i] == true)
            {
                var _prefab = Instantiate(rewardPref, rewardsGrid, false);
                _prefab.SetRewardData(activeRewards[i], rewards[i], levelStars[i]);

                yield return new WaitForSeconds(0.5f);                
            }           
        }
    }

    private void InitCatPrefab()
    {
        for (int i = 0; i < isRewardsShow.Length; i++)
        {
            if (isRewardsShow[i] == true)
                return;
        }

        if (!isStreet)
        {
                if (miniGameStars.IsCatHappy == true)
                    Instantiate(happyCatHead, rewardsGrid);
                else Instantiate(sadCatHead, rewardsGrid);
        }            
    }
}
