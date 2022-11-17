using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodUpgradeHandler : MonoBehaviour, IClickable
{
    public GameObject plateLevel1;
    public GameObject plateLevel2;
    public GameObject plateLevel3;
    public string PlateUpgradePrefFloor;
    public int price;
    public int price2;
    public int purch;
    private int TotalScore;
    public FocusScript onFocus;
    public GameObject upgradeParticles;
    public float yCorrection;


    private void Awake()
    {
        purch = PlayerPrefs.GetInt(PlateUpgradePrefFloor, 0);
        // Debug.Log("PlayerPrefs.GetInt(ppnamePower) " + purch);
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        if (purch == 0)
        {
            plateLevel1.SetActive(true);
            plateLevel2.SetActive(false);
            plateLevel3.SetActive(false);
            onFocus.foodTimerActive = 1;
        }
        if (purch == 1)
        {
            plateLevel1.SetActive(false);
            plateLevel2.SetActive(true);
            plateLevel3.SetActive(false);
            onFocus.foodTimerActive = 2;
        }
        if (purch == 2)
        {
            plateLevel1.SetActive(false);
            plateLevel2.SetActive(false);
            plateLevel3.SetActive(true);
            onFocus.foodTimerActive = 3;
        }
        TotalScore = PlayerPrefs.GetInt("TotalScore");
    }


    public void Click()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        purch = PlayerPrefs.GetInt(PlateUpgradePrefFloor, 0);
        if (TotalScore >= price && purch == 0)
        {
            Instantiate(upgradeParticles, new Vector3(transform.position.x, transform.position.y + yCorrection, 0), Quaternion.identity);
            PlayerPrefs.SetInt(PlateUpgradePrefFloor, 1);
            plateLevel1.SetActive(false);
            plateLevel2.SetActive(true);
            plateLevel3.SetActive(false);
            TotalScore = TotalScore - price;
            SoundManager.snd.PlaybuySounds();
            PlayerPrefs.SetInt("TotalScore", TotalScore);
        }
        if (TotalScore >= price2 && purch == 1)
        {
            Instantiate(upgradeParticles, new Vector3(transform.position.x, transform.position.y + yCorrection, 0), Quaternion.identity);
            PlayerPrefs.SetInt(PlateUpgradePrefFloor, 2);
            plateLevel1.SetActive(false);
            plateLevel2.SetActive(false);
            plateLevel3.SetActive(true);
            TotalScore = TotalScore - price2;
            SoundManager.snd.PlaybuySounds();
            PlayerPrefs.SetInt("TotalScore", TotalScore);
        }
        purch = PlayerPrefs.GetInt(PlateUpgradePrefFloor, 0);
    }
    public bool isFoodInPlate()
    {
        if (onFocus.foodTimerActive == 1 && onFocus.foodTimer1.IsFoodEnd() == false)
        {
            return true;
        }
        if (onFocus.foodTimerActive == 2 && onFocus.foodTimer2.IsFoodEnd() == false)
        {
            return true;
        }
        if (onFocus.foodTimerActive == 3 && onFocus.foodTimer3.IsFoodEnd() == false)
        {
            return true;
        }
        else return false;
    }
    public void ResetFood()
    {
        PlayerPrefs.SetInt(PlateUpgradePrefFloor, 0);
    }
}