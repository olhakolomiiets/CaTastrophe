using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController Instance;

    public int Coins
    {
        get => PlayerPrefs.GetInt("TotalScore", 0);
        private set => PlayerPrefs.SetInt("TotalScore", value);
    }

    public int Sand
    {
        get => PlayerPrefs.GetInt("TotalSand", 0);
        private set => PlayerPrefs.SetInt("TotalSand", value);
    }

    public int Food
    {
        get => PlayerPrefs.GetInt("TotalFood", 0);
        private set => PlayerPrefs.SetInt("TotalFood", value);
    }

    public int Energy
    {
        get => PlayerPrefs.GetInt("countPowersToRestore", 0);
        private set => PlayerPrefs.SetInt("countPowersToRestore", value);
    }

    [SerializeField] private PowersRestore energyRecovery;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddCoins(int value)
    {
        Coins += value;
    }

    public void AddSand(int value)
    {
        Sand += value;
    }

    public void AddFood(int value)
    {
        Food += value;
    }

    public void AddEnergy(int value)
    {
        Energy += value;
        energyRecovery.UpdateUI();
    }

    private void UpdateUI()
    {

    }
}
