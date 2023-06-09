using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using UnityEngine.Purchasing;

public class PurchaseManager : MonoBehaviour
{
    public bool isPurchased;
    public string purchaseName;
    public int purch;
    private int TotalScore;
    public GameObject buyTxt;
    public GameObject alreadyBoughtTxt;

    //private BannerView bannerAd;
    //private InterstitialAd interstitialAd;
    private bool adsDisabled = false;

    public void MoneyPack5000()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        PlayerPrefs.SetInt(purchaseName, 1);
        TotalScore = TotalScore + purch;
        Debug.Log("TotalScore" + TotalScore);
        SoundManager.snd.PlaybuySounds();
        buyTxt.SetActive(false);           
        alreadyBoughtTxt.SetActive(true);
        PlayerPrefs.SetInt("TotalScore", TotalScore);
    }

    public void ExtraLife()
    {
        if (PlayerPrefs.GetInt("extraLife") == 0)
        {
            PlayerPrefs.SetInt(purchaseName, 1);
            isPurchased = true;
            SoundManager.snd.PlaybuySounds();
            buyTxt.SetActive(false);
            alreadyBoughtTxt.SetActive(true);
        }
    }

    public void NoAds()
    {
        if (PlayerPrefs.GetInt("adsRemoved") == 0)
        {
            PlayerPrefs.SetInt(purchaseName, 1);
        }
    }

    public void OnPurchaseFailed()
    {
        Debug.Log($"Failed to purchase product with error: ");
    }

}
