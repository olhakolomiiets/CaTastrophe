[System.Serializable]

public class MiniGameReward
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
