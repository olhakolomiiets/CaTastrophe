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

    [Header("Powers To Restore")]
    [SerializeField] private float powersToRestore;

    private static IStoreController _storeController;
    private static IExtensionProvider _extensionProvider;

    private string noAds = "com.catastrophe.noads";
    private string extraLife = "com.catastrophe.extralife";
    private string moneyPack5000 = "com.catastrophe.moneypack5000";
    private string powerRestore = "com.catastrophe.powerstorestore";
    

    void Start()
    {
        InitializePurchasing();

/*        if (PlayerPrefs.HasKey("firstStart") == false)
        {
            PlayerPrefs.SetInt("firstStart", 1);
            RestoreMyProduct();
        }*/

        RestoreVariable();
    }

    void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(noAds, ProductType.NonConsumable);
        builder.AddProduct(extraLife, ProductType.NonConsumable);
        builder.AddProduct(moneyPack5000, ProductType.Consumable);
        builder.AddProduct(powerRestore, ProductType.Consumable);

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
        _storeController.InitiatePurchase(productName);
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

        if (product.definition.id == powerRestore)
        {
            Product_PowersToRestore();
        }

        Debug.Log($"Purchase Complete - Product: {product.definition.id}");

        return PurchaseProcessingResult.Complete;
    }

    private void Product_NoAds()
    {
        PlayerPrefs.SetInt("adsRemoved", 1);
        buyNoAdsTxt.SetActive(false);
        alreadyBoughtNoAdsTxt.SetActive(true);
        priceNoAds.SetActive(false);
        doneNoAds.SetActive(true);

    }

    private void Product_ExtraLife()
    {
        PlayerPrefs.SetInt("extraLife", 1);
        SoundManager.snd.PlaybuySounds();
        buyExtraLifeTxt.SetActive(false);
        alreadyBoughtExtraLifeTxt.SetActive(true);
        priceExtraLife.SetActive(false);
        doneExtraLife.SetActive(true);
        Debug.Log("!!!--- Extra Life Added ---!!!");
    }

    private void Product_MoneyPack5000()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        TotalScore = TotalScore + purch;
        Debug.Log("!!!--- TotalScore + MoneyPack 5000 ---!!! " + TotalScore);
        SoundManager.snd.PlaybuySounds();
        PlayerPrefs.SetInt("TotalScore", TotalScore);
    }
    private void Product_PowersToRestore()
    {
        powersToRestore = PlayerPrefs.GetFloat("countPowersToRestore");
        powersToRestore++;
        PlayerPrefs.SetFloat("countPowersToRestore", powersToRestore);
        Debug.Log("!!!--- Powers To Restore Added ---!!! " + powersToRestore);
        SoundManager.snd.PlaybuySounds();
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
        _storeController = controller;
        _extensionProvider = extensions;
    }


/*    public void RestoreMyProduct()
    {
        if (CodelessIAPStoreListener.Instance.StoreController.products.WithID(noAds).hasReceipt)
        {
            
        }

        if (CodelessIAPStoreListener.Instance.StoreController.products.WithID(extraLife).hasReceipt)
        {
           
        }
    }*/
}