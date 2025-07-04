using Firebase.Analytics;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;

public class PurchaseManager : MonoBehaviour
{
    private int TotalScore;

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

    [SerializeField] private Samples.Purchasing.IAP5.Demo.PaywallManager _purchaseController;

    [HideInInspector] public UnityEvent PurchasedProductExtraLife;
    [HideInInspector] public UnityEvent PurchasedProductMoneyPack2000;
    [HideInInspector] public UnityEvent PurchasedProductMoneyPack5000;
    [HideInInspector] public UnityEvent PurchasedProductMoneyPack10000;

    private ScoreManager sm;
    private void Awake()
    {
        _purchaseController = FindAnyObjectByType<Samples.Purchasing.IAP5.Demo.PaywallManager>();
    }

    // private void OnEnable()
    // {
    //     _purchaseController.PurchasedProductExtraLife.AddListener(ExtraLife);
    //     _purchaseController.PurchasedProductMoneyPack2000.AddListener(MoneyPack2000);
    //     _purchaseController.PurchasedProductMoneyPack5000.AddListener(MoneyPack5000);
    //     _purchaseController.PurchasedProductMoneyPack10000.AddListener(MoneyPack10000);
    // }
    private void Start()
    {
        sm = FindAnyObjectByType<ScoreManager>();
        RestoreVariable();
    }

    // public void BuyProduct(string productName)
    // {
    //     var _productNane = productName;
    //     _purchaseController.BuyProduct(_productNane);
    // }

    public void ExtraLife()
    {
        PlayerPrefs.SetInt("extraLife", 1);
        SoundManager.snd.PlaybuySounds();
        buyExtraLifeTxt.SetActive(false);
        alreadyBoughtExtraLifeTxt.SetActive(true);
        priceExtraLife.SetActive(false);
        doneExtraLife.SetActive(true);
        extraLifeWindow.SetActive(false);     
    }

    public void MoneyPack2000()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        TotalScore = TotalScore + buy2K;
        sm.UpdateAwardTotalScore(buy2K);
        SoundManager.snd.PlaybuySounds();
        PlayerPrefs.SetInt("TotalScore", TotalScore);
        _2KCoinsWindow.SetActive(false);       
    }

    public void MoneyPack5000()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        TotalScore = TotalScore + buy5K;
        sm.UpdateAwardTotalScore(buy5K);
        SoundManager.snd.PlaybuySounds();
        PlayerPrefs.SetInt("TotalScore", TotalScore);

        _5KCoinsWindow.SetActive(false);       
    }

    public void MoneyPack10000()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        TotalScore = TotalScore + buy10K;
        sm.UpdateAwardTotalScore(buy10K);
        SoundManager.snd.PlaybuySounds();
        PlayerPrefs.SetInt("TotalScore", TotalScore);
        _10KCoinsWindow.SetActive(false);        
    }

    void RestoreVariable()
    {
        if (PlayerPrefs.GetInt("extraLife") == 1)
        {
            buyExtraLifeTxt.SetActive(false);
            alreadyBoughtExtraLifeTxt.SetActive(true);
            priceExtraLife.SetActive(false);
            doneExtraLife.SetActive(true);
        }
    }

    // private void OnDisable()
    // {
    //     _purchaseController.PurchasedProductExtraLife.RemoveListener(ExtraLife);
    //     _purchaseController.PurchasedProductMoneyPack2000.RemoveListener(MoneyPack2000);
    //     _purchaseController.PurchasedProductMoneyPack5000.RemoveListener(MoneyPack5000);
    //     _purchaseController.PurchasedProductMoneyPack10000.RemoveListener(MoneyPack10000);
    // }

}
