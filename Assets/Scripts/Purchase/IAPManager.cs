using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class IAPManager : MonoBehaviour, IStoreListener
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

    private static IStoreController _storeController;
    private static IExtensionProvider _extensionProvider;

    private string noAds = "com.catastrophe.noads";
    private string extraLife = "com.catastrophe.extralife";
    private string moneyPack2000 = "com.catastrophe.moneypack2000";
    private string moneyPack5000 = "com.catastrophe.moneypack5k";
    private string moneyPack10000 = "com.catastrophe.moneypack10000";
    private string powerRestore = "com.catastrophe.powerstorestore";

    private string moneyPack5000ForPreRegistration = "com.catastrophe.moneypack5000";


    void Start()
    {

        if (_storeController == null)
        {
            InitializePurchasing();
        }

        /*        if (PlayerPrefs.HasKey("firstStart") == false)
                {
                    PlayerPrefs.SetInt("firstStart", 1);
                    RestoreMyProduct();
                }*/



        RestoreVariable();
    }

    void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(noAds, ProductType.NonConsumable);
        builder.AddProduct(extraLife, ProductType.NonConsumable);
        builder.AddProduct(moneyPack5000, ProductType.Consumable);
        builder.AddProduct(powerRestore, ProductType.Consumable);
        builder.AddProduct(moneyPack2000, ProductType.Consumable);
        builder.AddProduct(moneyPack10000, ProductType.Consumable);

        builder.AddProduct(moneyPack5000ForPreRegistration, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public void BuyProduct(string productName)
    {
        if (IsInitialized())
        {
            Product product = _storeController.products.WithID(productName);

            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                _storeController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    #region PURCHASE CONTROL
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        var product = args.purchasedProduct;

        if (String.Equals(product.definition.id, noAds, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", product.definition.id));

            if (PlayerPrefs.HasKey("adsRemoved") == false)
            {
                Product_NoAds();
            }
        }
        else if (String.Equals(product.definition.id, extraLife, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", product.definition.id));

            if (PlayerPrefs.HasKey("extraLife") == false)
            {
                Product_ExtraLife();
            }           
        }
        else if (String.Equals(product.definition.id, moneyPack2000, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", product.definition.id));

            Product_MoneyPack2000();
        }
        else if (String.Equals(product.definition.id, moneyPack5000, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", product.definition.id));

            Product_MoneyPack5000();
        }
        else if (String.Equals(product.definition.id, moneyPack10000, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", product.definition.id));

            Product_MoneyPack10000();
        }
        else if (String.Equals(product.definition.id, powerRestore, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", product.definition.id));

            Product_PowersToRestore();
        }
        else if (String.Equals(product.definition.id, moneyPack5000ForPreRegistration, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", product.definition.id));

                Product_MoneyPack5000ForPreRegistration();
        }
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", product.definition.id));
        }

        return PurchaseProcessingResult.Complete;
    }
    #endregion

    #region IF PURCHASE SUCCESSFUL
    private void Product_NoAds()
    {
        PlayerPrefs.SetInt("adsRemoved", 1);
        buyNoAdsTxt.SetActive(false);
        alreadyBoughtNoAdsTxt.SetActive(true);
        priceNoAds.SetActive(false);
        doneNoAds.SetActive(true);
        noAdsWindow.SetActive(false);
    }

    private void Product_ExtraLife()
    {
        PlayerPrefs.SetInt("extraLife", 1);
        SoundManager.snd.PlaybuySounds();
        buyExtraLifeTxt.SetActive(false);
        alreadyBoughtExtraLifeTxt.SetActive(true);
        priceExtraLife.SetActive(false);
        doneExtraLife.SetActive(true);
        extraLifeWindow.SetActive(false);
        Debug.Log("!!!--- Extra Life Added ---!!!");
    }

    private void Product_MoneyPack2000()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        TotalScore = TotalScore + buy2K;
        Debug.Log("!!!--- TotalScore + MoneyPack 2000 ---!!! " + TotalScore);
        SoundManager.snd.PlaybuySounds();
        PlayerPrefs.SetInt("TotalScore", TotalScore);
        _2KCoinsWindow.SetActive(false);
    }

    private void Product_MoneyPack5000()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        TotalScore = TotalScore + buy5K;
        Debug.Log("!!!--- TotalScore + MoneyPack 5000 ---!!! " + TotalScore);
        SoundManager.snd.PlaybuySounds();
        PlayerPrefs.SetInt("TotalScore", TotalScore);
        _5KCoinsWindow.SetActive(false);
    }

    private void Product_MoneyPack10000()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        TotalScore = TotalScore + buy10K;
        Debug.Log("!!!--- TotalScore + MoneyPack 10000 ---!!! " + TotalScore);
        SoundManager.snd.PlaybuySounds();
        PlayerPrefs.SetInt("TotalScore", TotalScore);
        _10KCoinsWindow.SetActive(false);
    }
    private void Product_PowersToRestore()
    {
        powersToRestore = PlayerPrefs.GetFloat("countPowersToRestore");
        powersToRestore = powersToRestore + amountPowersToRestore;
        PlayerPrefs.SetFloat("countPowersToRestore", powersToRestore);
        Debug.Log("!!!--- Powers To Restore Added ---!!! " + powersToRestore);
        SoundManager.snd.PlaybuySounds();
        restoreWindow.SetActive(false);
    }

    private void Product_MoneyPack5000ForPreRegistration()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        TotalScore = TotalScore + 5000;
        Debug.Log("!!!--- TotalScore + MoneyPack 5000 For Pre Registration ---!!! " + TotalScore);
        SoundManager.snd.PlaybuySounds();
        PlayerPrefs.SetInt("TotalScore", TotalScore);
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
    #endregion

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("In-App Purchasing successfully initialized");
        _storeController = controller;
        _extensionProvider = extensions;
    }

    private bool IsInitialized()
    {
        return _storeController != null && _extensionProvider != null;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log($"In-App Purchasing initialize failed: {error}");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
    }


    /*    public void RestoreMyProduct()
        {
            if (CodelessIAPStoreListener.Instance.StoreController.products.WithID(noAds).hasReceipt)
            {

            }

            if (CodelessIAPStoreListener.Instance.StoreController.products.WithID(extraLife).hasReceipt)
            {

            }

                if (CodelessIAPStoreListener.Instance.StoreController.products.WithID(moneyPack5000ForPreRegistration).hasReceipt)
            {

            }
        }*/
}