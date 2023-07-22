using Firebase.Analytics;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;

public class SpecialOffers : MonoBehaviour
{

    private int TotalScore;
    [SerializeField] SpecialOffersTimer specialOffersTimer;

    [Header("Energy Recovery")]
    [SerializeField] private GameObject restoreWindow;
    [SerializeField] private float powersToRestore;
    [SerializeField] private int amountPowersToRestore;

    [Header("Money Packs")]
    [SerializeField] private GameObject _3500CoinsWindow;
    [SerializeField] private int buy3500;

    [SerializeField] private IAPManager _purchaseController;

    [HideInInspector] public UnityEvent PurchasedSpecialOfferEnergyRecovery;
    [HideInInspector] public UnityEvent PurchasedSpecialOfferMoneyPack3500;

    private void Awake()
    {
        _purchaseController = FindObjectOfType<IAPManager>();
    }

    private void OnEnable()
    {
        _purchaseController.PurchasedSpecialOfferEnergyRecovery.AddListener(SpecialOfferEnergyRestore);
        _purchaseController.PurchasedSpecialOfferMoneyPack3500.AddListener(MoneyPack3500);

        Debug.Log("     O -----     Purchase Manager OnEnable     ----- O     ");
    }

    public void BuyProduct(string productName)
    {
        var _productNane = productName;
        _purchaseController.BuyProduct(_productNane);

        Debug.Log("     O -----     Purchase Manager Buy Product     ----- O     " + _productNane);
    }

    private void SpecialOfferEnergyRestore()
    {
        powersToRestore = PlayerPrefs.GetFloat("countPowersToRestore");
        powersToRestore = powersToRestore + amountPowersToRestore;
        PlayerPrefs.SetFloat("countPowersToRestore", powersToRestore);
        Debug.Log("!!!--- Powers To Restore Added ---!!! " + powersToRestore);
        SoundManager.snd.PlaybuySounds();
        restoreWindow.SetActive(false);

        specialOffersTimer.SpecialOfferPurchased();
    }

    private void MoneyPack3500()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        TotalScore = TotalScore + buy3500;
        Debug.Log("!!!--- TotalScore + MoneyPack 3500 ---!!! " + TotalScore);
        SoundManager.snd.PlaybuySounds();
        PlayerPrefs.SetInt("TotalScore", TotalScore);
        _3500CoinsWindow.SetActive(false);

        specialOffersTimer.SpecialOfferPurchased();
    }

    private void OnDisable()
    {
        _purchaseController.PurchasedSpecialOfferEnergyRecovery.RemoveListener(SpecialOfferEnergyRestore);
        _purchaseController.PurchasedSpecialOfferMoneyPack3500.RemoveListener(MoneyPack3500);
    }

}
