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

    private void Awake()
    {
        isRemainderAlreadyShow = false;
    }

    void Start()
    {        
        Invoke("CheckToiletAndFood", 1.5f);
        Invoke("CheckDailyRewards", 2f);
    }

    public void CheckToiletAndFood()
    {
        if (PassivePowerUp.FoodSpeeUp == true || PassivePowerUp.Food2SpeeUp == true || PassivePowerUp.Food3SpeeUp == true)
        {

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
        if (dailyRewards.canClaimReward)
        {
            Remainder3Ico.SetActive(true);
            if (isRemainderAlreadyShow) return;
            windowUIbackgr.SetActive(true);
            Remainder3Txt.SetActive(true);
            isRemainderAlreadyShow = true;
        }
    }

    public void UpdateDailyRewards()
    {
        if (!dailyRewards.canClaimReward)
        {
            windowUIbackgr.SetActive(false);
            Remainder3Txt.SetActive(false);
            Remainder3Ico.SetActive(false);
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

    public void CloseReminderUI()
    {
        windowUIbackgr.SetActive(false);
    }

}
