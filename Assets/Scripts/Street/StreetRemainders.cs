using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StreetRemainders : MonoBehaviour
{
    [Header("Remainder Feed Cats")]
    [SerializeField] private GameObject Remainder1Txt;
    [SerializeField] private GameObject Remainder1Ico;

    [Header("Remainder Clean After Cats")]
    [SerializeField] private GameObject Remainder2Txt;
    [SerializeField] private GameObject Remainder2Ico;

    [Header("Remainder Daily Rewards")]
    [SerializeField] private GameObject Remainder3Txt;
    [SerializeField] private GameObject Remainder3Ico;

    [Header("Managers")]
    [SerializeField] private PassivePowerUp passivePowerUpManager;
    [SerializeField] private GameObject windowUIbackgr;
    [SerializeField] private DailyRewards dailyRewards;

    private bool isRemainderAlreadyShow = false;



    void OnEnable()
    {        
        Invoke("CheckToiletAndFood", 1.5f);
        //CheckToiletAndFood();
        Invoke("CheckDailyRewards", 2f);
        //CheckDailyRewards();
    }

    public void CheckToiletAndFood()
    {
        if (PassivePowerUp.FoodSpeeUp == true || PassivePowerUp.Food2SpeeUp == true || PassivePowerUp.Food3SpeeUp == true)
        {
            windowUIbackgr.SetActive(false);
            Remainder1Txt.SetActive(false);
            Remainder1Ico.SetActive(false);
            isRemainderAlreadyShow = false;
        }
        else
        {
            if (isRemainderAlreadyShow) return;
            windowUIbackgr.SetActive(true);
            Remainder1Txt.SetActive(true);
            Remainder1Ico.SetActive(true);
            isRemainderAlreadyShow = true;
        }

        if (PassivePowerUp.ToiletSpeeUp == true || PassivePowerUp.Toilet2SpeeUp == true || PassivePowerUp.Toilet3SpeeUp == true)
        {
            windowUIbackgr.SetActive(false);
            Remainder2Txt.SetActive(false);
            Remainder2Ico.SetActive(false);
            isRemainderAlreadyShow = false;
        }
        else
        {
            if (isRemainderAlreadyShow) return;
            windowUIbackgr.SetActive(true);
            Remainder2Txt.SetActive(true);
            Remainder2Ico.SetActive(true);
            isRemainderAlreadyShow = true;
        }
    }

    public void CheckDailyRewards()
    {
        if (!dailyRewards.canClaimReward)
        {
            windowUIbackgr.SetActive(false);
            Remainder3Txt.SetActive(false);
            Remainder3Ico.SetActive(false);
            isRemainderAlreadyShow = false;
        }
        else
        {
            Remainder3Ico.SetActive(true);
            if (isRemainderAlreadyShow) return;
            windowUIbackgr.SetActive(true);
            Remainder3Txt.SetActive(true);            
            isRemainderAlreadyShow = true;
        }

    }

}
