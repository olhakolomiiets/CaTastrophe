using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameRewardPref : MonoBehaviour
{
    [SerializeField] private Text rewardValue;

    [Space(5)]
    [SerializeField] private Image rewardIcon;
    [SerializeField] private Sprite rewardCoins;
    [SerializeField] private Sprite rewardSand;
    [SerializeField] private Sprite rewardFood;
    [SerializeField] private Sprite rewardEnergyRecovery;

    [Space(5)]
    [SerializeField] private GameObject doneIcon;
    [SerializeField] private GameObject claimButton;

    private MiniGameReward reward;
    private List<MiniGameReward> rewards;
    private int star;
    private string levelStarsPrefs;

    public void SetRewardDataForStreet(int activeStar, MiniGameReward miniGameReward, string levelStars)
    {
        reward = miniGameReward;
        star = activeStar;
        levelStarsPrefs = levelStars;

        Debug.Log("levelStarsPrefs " + levelStarsPrefs + "   " + PlayerPrefs.GetInt(levelStarsPrefs));

        rewardIcon.sprite = reward.Type == MiniGameReward.RewardType.COINS ? rewardCoins : reward.Type == MiniGameReward.RewardType.SAND ? rewardSand : reward.Type == MiniGameReward.RewardType.FOOD ? rewardFood : rewardEnergyRecovery;
        rewardValue.text = $"{"x"}" + reward.Value.ToString();

        if (PlayerPrefs.GetInt(levelStarsPrefs) < 2)
        {
            doneIcon.SetActive(false);

            if (star == 1)
                claimButton.SetActive(true);
            else
                claimButton.SetActive(false);
        }
        else
        {
            doneIcon.SetActive(true);
            claimButton.SetActive(false);
        }           
    }    
    public void SetRewardData(int activeStar, MiniGameReward miniGameReward, string levelStars)
    {
            reward = miniGameReward;
            star = activeStar;
            levelStarsPrefs = levelStars;

            rewardIcon.sprite = reward.Type == MiniGameReward.RewardType.COINS ? rewardCoins : reward.Type == MiniGameReward.RewardType.SAND ? rewardSand : reward.Type == MiniGameReward.RewardType.FOOD ? rewardFood : rewardEnergyRecovery;
            rewardValue.text = $"{"x"}" + reward.Value.ToString();
            claimButton.SetActive(true);       
    }

    public void UpdateUI()
    {
        doneIcon.SetActive(true);
        claimButton.SetActive(false);
    }

    public void ClaimReward()
    {
        SoundManager.snd.PlayButtonsSound();

        switch (reward.Type)
        {
            case MiniGameReward.RewardType.COINS:
                MiniGameController.Instance.AddCoins(reward.Value);
                break;
            case MiniGameReward.RewardType.SAND:
                MiniGameController.Instance.AddSand(reward.Value);
                break;
            case MiniGameReward.RewardType.FOOD:
                MiniGameController.Instance.AddFood(reward.Value);
                break;
            case MiniGameReward.RewardType.ENERGY_RECOVERY:
                MiniGameController.Instance.AddEnergy(reward.Value);
                break;
        }

        var _rewardType = reward.Type.ToString();

        PlayerPrefs.SetInt(levelStarsPrefs, 2);

        UpdateUI();
    }

}
