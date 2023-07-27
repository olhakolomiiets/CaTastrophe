using UnityEngine;
using UnityEngine.UI;

public class DailyRewardPref : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Sprite defaultBackground;
    [SerializeField] private Sprite currentStreakBackground;


    [Space(5)]
    [SerializeField] private Text dayText;
    [SerializeField] private Text rewardValue;

    [Space(5)]
    [SerializeField] private Image rewardIcon;
    [SerializeField] private Sprite rewardCoins;
    [SerializeField] private Sprite rewardSand;
    [SerializeField] private Sprite rewardFood;
    [SerializeField] private Sprite rewardEnergyRecovery;

    [SerializeField] private GameObject doneIcon;

    public void SetRewardData(int day, int CurrentStreak, DailyReward reward)
    {
        dayText.text = $"Day {day + 1}";

        rewardIcon.sprite = reward.Type == DailyReward.RewardType.COINS ? rewardCoins : reward.Type == DailyReward.RewardType.SAND ? rewardSand : reward.Type == DailyReward.RewardType.FOOD ? rewardFood : rewardEnergyRecovery;
        rewardValue.text = reward.Value.ToString();

        background.sprite = day == CurrentStreak ? currentStreakBackground : defaultBackground;

        doneIcon.SetActive(false);
    }

    public void UpdateUI(int day, int CurrentStreak)
    {
        if (day < CurrentStreak)
        {
            doneIcon.SetActive(true);
        }

    }
}
