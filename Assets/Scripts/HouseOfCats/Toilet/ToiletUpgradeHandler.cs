﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletUpgradeHandler : MonoBehaviour, IClickable
{
    public GameObject toiletLevel1;
    public GameObject toiletLevel2;
    public GameObject toiletLevel3;
    public string ToiletUpgradePrefFloor;
    [SerializeField] private UpgradePricesSO _upgradePrices;
    [SerializeField] private UpgradeToiletUI _upgradeToiletUI; 
    public int purch;
    public int floor;
    private int TotalScore;
    public FocusScript onFocus;
    public GameObject upgradeParticles;
    public float yCorrection;

    #region Properties

    public UpgradePricesSO UpgradePrices => _upgradePrices;

    #endregion

    void Start()
    {
        purch = PlayerPrefs.GetInt(ToiletUpgradePrefFloor, 0);
        UpdateToiletLevel();

        TotalScore = PlayerPrefs.GetInt("TotalScore");
    }

    public void Click()
    {
        _upgradeToiletUI.CurrentToiletUpgradeHandler = this; 
        _upgradeToiletUI.gameObject.SetActive(true);
    }
    public void Upgrade()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        purch = PlayerPrefs.GetInt(ToiletUpgradePrefFloor, 0);

        if (TotalScore >= _upgradePrices.UpgradePriceToiletLvl2 && purch == 0)
        {
            Instantiate(upgradeParticles, new Vector3(transform.position.x, transform.position.y + yCorrection, 0), Quaternion.identity);
            PlayerPrefs.SetInt(ToiletUpgradePrefFloor, 1);
            toiletLevel1.SetActive(false);
            toiletLevel2.SetActive(true);
            toiletLevel3.SetActive(false);
            TotalScore = TotalScore - _upgradePrices.UpgradePriceToiletLvl2;
            SoundManager.snd.PlaybuySounds();
            PlayerPrefs.SetInt("TotalScore", TotalScore);
        }
        if (TotalScore >= _upgradePrices.UpgradePriceToiletLvl3 && purch == 1)
        {
            Instantiate(upgradeParticles, new Vector3(transform.position.x, transform.position.y + yCorrection, 0), Quaternion.identity);
            PlayerPrefs.SetInt(ToiletUpgradePrefFloor, 2);
            toiletLevel1.SetActive(false);
            toiletLevel2.SetActive(false);
            toiletLevel3.SetActive(true);
            TotalScore = TotalScore - _upgradePrices.UpgradePriceToiletLvl3;
            SoundManager.snd.PlaybuySounds();
            PlayerPrefs.SetInt("TotalScore", TotalScore);
        }
        purch = PlayerPrefs.GetInt(ToiletUpgradePrefFloor, 0);

        UpdateToiletLevel();

        switch (floor)
        {
        case 3:
            PowerForToilet3.instance.UpdateMsToiletTime();
            break;
        case 2:
            PowerForToilet2.instance.UpdateMsToiletTime();
            break;
        case 1:
            PowerForToilet.instance.UpdateMsToiletTime();
            break;
        default:
            PowerForToilet.instance.UpdateMsToiletTime();
            break;
        }

        _upgradeToiletUI.gameObject.SetActive(false);
    }

    public void ResetToilet()
    {
        PlayerPrefs.SetInt(ToiletUpgradePrefFloor, 0);
    }

    public void UpdateToiletLevel()
    {
        if (purch == 0)
        {
            toiletLevel1.SetActive(true);
            toiletLevel2.SetActive(false);
            toiletLevel3.SetActive(false);
            onFocus.toiletTimerActive = 1;
        }
        if (purch == 1)
        {
            toiletLevel1.SetActive(false);
            toiletLevel2.SetActive(true);
            toiletLevel3.SetActive(false);
            onFocus.toiletTimerActive = 2;
        }
        if (purch == 2)
        {
            toiletLevel1.SetActive(false);
            toiletLevel2.SetActive(false);
            toiletLevel3.SetActive(true);
            onFocus.toiletTimerActive = 3;
        }
    }
}
