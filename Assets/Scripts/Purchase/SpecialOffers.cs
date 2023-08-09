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
    [SerializeField] private int powersToRestore;
    [SerializeField] private int amountPowersToRestore;

    [Header("Money Packs")]
    [SerializeField] private GameObject _3500CoinsWindow;
    [SerializeField] private int buy3500;

    [Header("Starter Pack")]
    [SerializeField] private GameObject _starterPackWindow;
    private int _food;
    private int _sand;
    [SerializeField] private PowersRestore energyRecovery;
    [SerializeField] private StarterPackTimer _starterPackTimer;

    [Header("Extra Life")]
    [SerializeField] private GameObject _extraLifeWindow;

    [HideInInspector] public UnityEvent PurchasedProductExtraLifeSpecial;


    [Space(5)]
    [SerializeField] private IAPManager _purchaseController;

    [HideInInspector] public UnityEvent PurchasedSpecialOfferEnergyRecovery;
    [HideInInspector] public UnityEvent PurchasedSpecialOfferMoneyPack3500;
    [HideInInspector] public UnityEvent PurchasedSpecialOfferStarterPack;

    

    private void Awake()
    {
        _purchaseController = FindObjectOfType<IAPManager>();
    }

    private void OnEnable()
    {
        _purchaseController.PurchasedSpecialOfferEnergyRecovery.AddListener(SpecialOfferEnergyRestore);
        _purchaseController.PurchasedSpecialOfferMoneyPack3500.AddListener(MoneyPack3500);
        _purchaseController.PurchasedSpecialOfferStarterPack.AddListener(StarterPack);
        _purchaseController.PurchasedProductExtraLifeSpecial.AddListener(GetExtraLife);
    }

    public void BuyProduct(string productName)
    {
        var _productNane = productName;
        _purchaseController.BuyProduct(_productNane);
    }

    private void SpecialOfferEnergyRestore()
    {
        powersToRestore = PlayerPrefs.GetInt("countPowersToRestore");
        powersToRestore = powersToRestore + amountPowersToRestore;
        PlayerPrefs.SetInt("countPowersToRestore", powersToRestore);
        SoundManager.snd.PlaybuySounds();
        restoreWindow.SetActive(false);

        energyRecovery.UpdateUI();

        specialOffersTimer.SpecialOfferPurchased();
    }

    private void MoneyPack3500()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        TotalScore = TotalScore + buy3500;
        SoundManager.snd.PlaybuySounds();
        PlayerPrefs.SetInt("TotalScore", TotalScore);
        _3500CoinsWindow.SetActive(false);

        specialOffersTimer.SpecialOfferPurchased();
    }

    private void StarterPack()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        TotalScore = TotalScore + 10000;
        PlayerPrefs.SetInt("TotalScore", TotalScore);

        powersToRestore = PlayerPrefs.GetInt("countPowersToRestore");
        powersToRestore = powersToRestore + 5;
        PlayerPrefs.SetInt("countPowersToRestore", powersToRestore);
        
        energyRecovery.UpdateUI();

        _food = PlayerPrefs.GetInt("TotalFood");
        _food = _food + 20;
        PlayerPrefs.SetInt("TotalFood", _food);

        _sand = PlayerPrefs.GetInt("TotalSand");
        _sand = _sand + 20;
        PlayerPrefs.SetInt("TotalSand", _sand);

        PlayerPrefs.SetInt("DSOSlimCat", 1);
        PlayerPrefs.SetInt("CatsIsYoursAchieve", PlayerPrefs.GetInt("CatsIsYoursAchieve") + 1);

        SoundManager.snd.PlaybuySounds();
        _starterPackWindow.SetActive(false);
        _starterPackTimer.HideButton();

        PlayerPrefs.SetInt("UserGotStarterPack", 1);
    }

    private void GetExtraLife()
    {
        PlayerPrefs.SetInt("extraLife", 1);
        SoundManager.snd.PlaybuySounds();
        _extraLifeWindow.SetActive(false);
    }

    private void OnDisable()
    {
        _purchaseController.PurchasedSpecialOfferEnergyRecovery.RemoveListener(SpecialOfferEnergyRestore);
        _purchaseController.PurchasedSpecialOfferMoneyPack3500.RemoveListener(MoneyPack3500);
        _purchaseController.PurchasedSpecialOfferStarterPack.RemoveListener(StarterPack);
        _purchaseController.PurchasedProductExtraLifeSpecial.RemoveListener(GetExtraLife);
    }

}
