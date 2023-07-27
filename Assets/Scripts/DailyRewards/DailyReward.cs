[System.Serializable]

public class DailyReward
{
    public enum RewardType
    {
        COINS,
        SAND,
        FOOD,
        ENERGY_RECOVERY
    }

    public RewardType Type;
    public int Value;
    public string Name;
}
