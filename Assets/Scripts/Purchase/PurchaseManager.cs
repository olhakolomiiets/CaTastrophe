using Firebase.Analytics;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;

public class PurchaseManager : MonoBehaviour
{
    private int TotalScore;

    [Header("No Ads")]
    [SerializeField] private GameObject noAdsWindow;
    [SerializeField] private GameObject buyNoAdsTxt;
    [SerializeField] private GameObject alreadyBoughtNoAdsTxt;
    [SerializeField] private GameObject priceNoAds;
    [SerializeField] private GameObject doneNoAds;

    [Header("Extra Life")]
    [SerializeField] private GameObject extraLifeWindow;
    [SerializeField] private GameObject buyExtraLifeTxt;
    [SerializeField] private GameObject alreadyBoughtExtraLifeTxt;
    [SerializeField] private GameObject priceExtraLife;
    [SerializeField] private GameObject doneExtraLife;

    [Header("Money Packs")]
    [SerializeField] private GameObject _2KCoinsWindow;
    [SerializeField] private int buy2K;
    [SerializeField] private GameObject _5KCoinsWindow;
    [SerializeField] private int buy5K;
    [SerializeField] private GameObject _10KCoinsWindow;
    [SerializeField] private int buy10K;

    [Header("Powers To Restore")]
    [SerializeField] private GameObject restoreWindow;
    [SerializeField] private float powersToRestore;
    [SerializeField] private int amountPowersToRestore;

    [SerializeField] private IAPManager _purchaseController;

    [HideInInspector] public UnityEvent PurchasedProductNoAds;
    [HideInInspector] public UnityEvent PurchasedProductExtraLife;
    [HideInInspector] public UnityEvent PurchasedProductMoneyPack2000;
    [HideInInspector] public UnityEvent PurchasedProductMoneyPack5000;
    [HideInInspector] public UnityEvent PurchasedProductMoneyPack10000;
    [HideInInspector] public UnityEvent PurchasedProductPowersToRestore;

    private void Awake()
    {
        _purchaseController = FindObjectOfType<IAPManager>();
    }

    private void OnEnable()
    {
        _purchaseController.PurchasedProductNoAds.AddListener(NoAds);
        _purchaseController.PurchasedProductExtraLife.AddListener(ExtraLife);
        _purchaseController.PurchasedProductMoneyPack2000.AddListener(MoneyPack2000);
        _purchaseController.PurchasedProductMoneyPack5000.AddListener(MoneyPack5000);
        _purchaseController.PurchasedProductMoneyPack10000.AddListener(MoneyPack10000);
        _purchaseController.PurchasedProductPowersToRestore.AddListener(PowersToRestore);

        Debug.Log("     O -----     Purchase Manager OnEnable     ----- O     ");
    }
    private void Start()
    {
        RestoreVariable();
    }

    public void BuyProduct(string productName)
    {
        var _productNane = productName;
        _purchaseController.BuyProduct(_productNane);

        Debug.Log("     O -----     Purchase Manager Buy Product     ----- O     " + _productNane);
    }

     private void NoAds()
    {
        PlayerPrefs.SetInt("adsRemoved", 1);
        buyNoAdsTxt.SetActive(false);
        alreadyBoughtNoAdsTxt.SetActive(true);
        priceNoAds.SetActive(false);
        doneNoAds.SetActive(true);
        noAdsWindow.SetActive(false);

        FirebaseAnalytics.LogEvent(name: "no_ads_purchased");
    }

    private void ExtraLife()
    {
        PlayerPrefs.SetInt("extraLife", 1);
        SoundManager.snd.PlaybuySounds();
        buyExtraLifeTxt.SetActive(false);
        alreadyBoughtExtraLifeTxt.SetActive(true);
        priceExtraLife.SetActive(false);
        doneExtraLife.SetActive(true);
        extraLifeWindow.SetActive(false);
        Debug.Log("!!!--- Extra Life Added ---!!!");

        FirebaseAnalytics.LogEvent(name: "extra_life_purchased");
    }

    private void MoneyPack2000()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        TotalScore = TotalScore + buy2K;
        Debug.Log("!!!--- TotalScore + MoneyPack 2000 ---!!! " + TotalScore);
        SoundManager.snd.PlaybuySounds();
        PlayerPrefs.SetInt("TotalScore", TotalScore);
        _2KCoinsWindow.SetActive(false);

        FirebaseAnalytics.LogEvent(name: "money_pack_2k_purchased");
    }

    private void MoneyPack5000()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        TotalScore = TotalScore + buy5K;
        Debug.Log("!!!--- TotalScore + MoneyPack 5000 ---!!! " + TotalScore);
        SoundManager.snd.PlaybuySounds();
        PlayerPrefs.SetInt("TotalScore", TotalScore);
        _5KCoinsWindow.SetActive(false);

        FirebaseAnalytics.LogEvent(name: "money_pack_5k_purchased");
    }

    private void MoneyPack10000()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        TotalScore = TotalScore + buy10K;
        Debug.Log("!!!--- TotalScore + MoneyPack 10000 ---!!! " + TotalScore);
        SoundManager.snd.PlaybuySounds();
        PlayerPrefs.SetInt("TotalScore", TotalScore);
        _10KCoinsWindow.SetActive(false);

        FirebaseAnalytics.LogEvent(name: "money_pack_10k_purchased");
    }
    private void PowersToRestore()
    {
        powersToRestore = PlayerPrefs.GetFloat("countPowersToRestore");
        powersToRestore = powersToRestore + amountPowersToRestore;
        PlayerPrefs.SetFloat("countPowersToRestore", powersToRestore);
        Debug.Log("!!!--- Powers To Restore Added ---!!! " + powersToRestore);
        SoundManager.snd.PlaybuySounds();
        restoreWindow.SetActive(false);

        FirebaseAnalytics.LogEvent(name: "powers_restore_purchased");
    }

    void RestoreVariable()
    {
        if (PlayerPrefs.HasKey("adsRemoved"))
        {
            buyNoAdsTxt.SetActive(false);
            alreadyBoughtNoAdsTxt.SetActive(true);
            priceNoAds.SetActive(false);
            doneNoAds.SetActive(true);
        }

        if (PlayerPrefs.HasKey("extraLife"))
        {
            buyExtraLifeTxt.SetActive(false);
            alreadyBoughtExtraLifeTxt.SetActive(true);
            priceExtraLife.SetActive(false);
            doneExtraLife.SetActive(true);
        }

    }

    private void OnDisable()
    {
        _purchaseController.PurchasedProductNoAds.RemoveListener(NoAds);
        _purchaseController.PurchasedProductExtraLife.RemoveListener(ExtraLife);
        _purchaseController.PurchasedProductMoneyPack2000.RemoveListener(MoneyPack2000);
        _purchaseController.PurchasedProductMoneyPack5000.RemoveListener(MoneyPack5000);
        _purchaseController.PurchasedProductMoneyPack10000.RemoveListener(MoneyPack10000);
        _purchaseController.PurchasedProductPowersToRestore.RemoveListener(PowersToRestore);
    }

}
