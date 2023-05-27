using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodUpgradeHandler : MonoBehaviour, IClickable
{
    public GameObject plateLevel1;
    public GameObject plateLevel2;
    public GameObject plateLevel3;
    public string PlateUpgradePrefFloor;
    [SerializeField] private UpgradePricesSO _upgradePrices;
    [SerializeField] private UpgradePlateUI _upgradePlateUI;
    public int purch;
    private int TotalScore;
    public FocusScript onFocus;
    public GameObject upgradeParticles;
    public float yCorrection;
    [SerializeField] private TimerForFood foodTimerLvl1;
    [SerializeField] private TimerForFood foodTimerLvl2;
    [SerializeField] private TimerForFood foodTimerLvl3;
    [SerializeField] private int foodTimerActive = 0;

    #region Properties

    public UpgradePricesSO UpgradePrices => _upgradePrices;

    #endregion

    private void Awake()
    {
        purch = PlayerPrefs.GetInt(PlateUpgradePrefFloor, 0);
        UpdatePlateLevel();
        TotalScore = PlayerPrefs.GetInt("TotalScore");
    }

    public void Click()
    {
        _upgradePlateUI.CurrentFoodUpgradeHandler = this;
        _upgradePlateUI.gameObject.SetActive(true);
    }

    public void Upgrade()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        purch = PlayerPrefs.GetInt(PlateUpgradePrefFloor, 0);
        if (TotalScore >= _upgradePrices.UpgradePriceFoodPlateLvl2 && purch == 0)
        {
            Instantiate(upgradeParticles, new Vector3(transform.position.x, transform.position.y + yCorrection, 0), Quaternion.identity);
            PlayerPrefs.SetInt(PlateUpgradePrefFloor, 1);
            plateLevel1.SetActive(false);
            plateLevel2.SetActive(true);
            plateLevel3.SetActive(false);
            TotalScore = TotalScore - _upgradePrices.UpgradePriceFoodPlateLvl2;
            SoundManager.snd.PlaybuySounds();
            PlayerPrefs.SetInt("TotalScore", TotalScore);
        }
        if (TotalScore >= _upgradePrices.UpgradePriceFoodPlateLvl3 && purch == 1)
        {
            Instantiate(upgradeParticles, new Vector3(transform.position.x, transform.position.y + yCorrection, 0), Quaternion.identity);
            PlayerPrefs.SetInt(PlateUpgradePrefFloor, 2);
            plateLevel1.SetActive(false);
            plateLevel2.SetActive(false);
            plateLevel3.SetActive(true);
            TotalScore = TotalScore - _upgradePrices.UpgradePriceFoodPlateLvl3;
            SoundManager.snd.PlaybuySounds();
            PlayerPrefs.SetInt("TotalScore", TotalScore);
        }
        purch = PlayerPrefs.GetInt(PlateUpgradePrefFloor, 0);
        UpdatePlateLevel();
        _upgradePlateUI.gameObject.SetActive(false);
    }
    public bool isFoodInPlate()
    {
        if (foodTimerActive == 1 && foodTimerLvl1.IsFoodEnd() == false)
        {
            return true;
        }
        if (foodTimerActive == 2 && foodTimerLvl2.IsFoodEnd() == false)
        {
            return true;
        }
        if (foodTimerActive == 3 && foodTimerLvl3.IsFoodEnd() == false)
        {
            return true;
        }
        else return false;
    }
    public void ResetFood()
    {
        PlayerPrefs.SetInt(PlateUpgradePrefFloor, 0);
    }

    public void UpdatePlateLevel()
    {
        if (purch == 0)
        {
            plateLevel1.SetActive(true);
            plateLevel2.SetActive(false);
            plateLevel3.SetActive(false);
            foodTimerActive = 1;
        }
        if (purch == 1)
        {
            plateLevel1.SetActive(false);
            plateLevel2.SetActive(true);
            plateLevel3.SetActive(false);
            foodTimerActive = 2;
        }
        if (purch == 2)
        {
            plateLevel1.SetActive(false);
            plateLevel2.SetActive(false);
            plateLevel3.SetActive(true);
            foodTimerActive = 3;
        }
    }
}