using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class IAPManager : MonoBehaviour, IStoreListener
{
    private int TotalScore;

    [Header("No Ads")]
    [SerializeField] private GameObject buyNoAdsTxt;
    [SerializeField] private GameObject alreadyBoughtNoAdsTxt;
    [SerializeField] private GameObject priceNoAds;
    [SerializeField] private GameObject doneNoAds;
    [Header("Extra Life")]
    [SerializeField] private GameObject buyExtraLifeTxt;
    [SerializeField] private GameObject alreadyBoughtExtraLifeTxt;
    [SerializeField] private GameObject priceExtraLife;
    [SerializeField] private GameObject doneExtraLife;
    [Header("Money Pack 5000")]
    [SerializeField] private int purch;
    [SerializeField] private GameObject buyMoneyPackTxt;
    [SerializeField] private GameObject alreadyBoughtMoneyPackTxt;


    IStoreController m_StoreController;

    private string noAds = "com.catastrophe.noads";
    private string extraLife = "com.catastrophe.extralife";
    private string moneyPack5000 = "com.catastrophe.moneypack5000";

    void Start()
    {
        InitializePurchasing();

        //if (PlayerPrefs.HasKey("firstStart") == false)
        //{
        //    PlayerPrefs.SetInt("firstStart", 1);
        //    RestoreMyProduct();
        //}

        RestoreVariable();
    }

    void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(noAds, ProductType.NonConsumable);
        builder.AddProduct(extraLife, ProductType.NonConsumable);
        builder.AddProduct(moneyPack5000, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
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

    public void BuyProduct(string productName)
    {
        m_StoreController.InitiatePurchase(productName);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        var product = args.purchasedProduct;

        if (product.definition.id == noAds)
        {
            Product_NoAds();
        }

        if (product.definition.id == extraLife)
        {
            Product_ExtraLife();
        }

        if (product.definition.id == moneyPack5000)
        {
            Product_MoneyPack5000();
        }

        Debug.Log($"Purchase Complete - Product: {product.definition.id}");

        return PurchaseProcessingResult.Complete;
    }

    private void Product_NoAds()
    {
        PlayerPrefs.SetInt("adsRemoved", 0);
        buyNoAdsTxt.SetActive(false);
        alreadyBoughtNoAdsTxt.SetActive(true);

    }

    private void Product_ExtraLife()
    {
        PlayerPrefs.SetInt("extraLife", 0);
        SoundManager.snd.PlaybuySounds();
        buyExtraLifeTxt.SetActive(false);
        alreadyBoughtExtraLifeTxt.SetActive(true);
        Debug.Log("!!!--- Extra Life Added ---!!!");
    }

    private void Product_MoneyPack5000()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        TotalScore = TotalScore + purch;
        Debug.Log("!!!--- TotalScore + MoneyPack 5000 ---!!! " + TotalScore);
        SoundManager.snd.PlaybuySounds();
        buyMoneyPackTxt.SetActive(false);
        alreadyBoughtMoneyPackTxt.SetActive(true);
        PlayerPrefs.SetInt("TotalScore", TotalScore);
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log($"In-App Purchasing initialize failed: {error}");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("In-App Purchasing successfully initialized");
        m_StoreController = controller;
    }


    //public void RestoreMyProduct()
    //{
    //    if (CodelessIAPStoreListener.Instance.StoreController.products.WithID(noAds).hasReceipt)
    //    {
    //        Product_NoAds();
    //    }

    //    if (CodelessIAPStoreListener.Instance.StoreController.products.WithID(extraLife).hasReceipt)
    //    {
    //        Product_ExtraLife();
    //    }
    //}
}