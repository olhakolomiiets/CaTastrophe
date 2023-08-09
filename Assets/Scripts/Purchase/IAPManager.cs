using Firebase.Analytics;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener
{
    public static IAPManager instance = null;

    private int TotalScore;

    private static IStoreController _storeController;
    private static IExtensionProvider _extensionProvider;

    private string noAds = "com.catastrophe.noads";
    private string extraLife = "com.catastrophe.extralife";
    private string moneyPack2000 = "com.catastrophe.moneypack2000";
    private string moneyPack5000 = "com.catastrophe.moneypack5k";
    private string moneyPack10000 = "com.catastrophe.moneypack10000";
    private string powerRestore = "com.catastrophe.powerstorestore";

    private string moneyPack5000ForPreRegistration = "com.catastrophe.moneypack5000";

    private string specialOfferEnergyRestore = "com.catastrophe.specialofferenergyrecovery";
    private string specialOfferMoneyPack3500 = "com.catastrophe.specialoffermoneypack3500";
    private string specialOfferStarterPack = "com.catastrophe.starterpack";

    private string extraLifeSpecial = "com.catastrophe.specialofferextralife";

    [HideInInspector] public UnityEvent PurchasedProductNoAds;
    [HideInInspector] public UnityEvent PurchasedProductExtraLife;
    [HideInInspector] public UnityEvent PurchasedProductMoneyPack2000;
    [HideInInspector] public UnityEvent PurchasedProductMoneyPack5000;
    [HideInInspector] public UnityEvent PurchasedProductMoneyPack10000;
    [HideInInspector] public UnityEvent PurchasedProductPowersToRestore;

    [HideInInspector] public UnityEvent PurchasedSpecialOfferEnergyRecovery;
    [HideInInspector] public UnityEvent PurchasedSpecialOfferMoneyPack3500;
    [HideInInspector] public UnityEvent PurchasedSpecialOfferStarterPack;

    [HideInInspector] public UnityEvent PurchasedProductExtraLifeSpecial;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Debug.Log("     O -----     IAP Manager Start     ----- O     ");
        if (_storeController == null)
        {
            Invoke("InitializePurchasing", 2f);
            //InitializePurchasing();
        }
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

        builder.AddProduct(specialOfferEnergyRestore, ProductType.Consumable);
        builder.AddProduct(specialOfferMoneyPack3500, ProductType.Consumable);
        builder.AddProduct(specialOfferStarterPack, ProductType.Consumable);

        builder.AddProduct(extraLifeSpecial, ProductType.NonConsumable);

        UnityPurchasing.Initialize(this, builder);

        Debug.Log("     O -----     IAP Manager InitializePurchasing     ----- O     ");
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
                PurchasedProductNoAds.Invoke();

                FirebaseAnalytics.LogEvent(name: "no_ads_purchased");
            }
        }
        else if (String.Equals(product.definition.id, extraLife, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", product.definition.id));

            if (PlayerPrefs.HasKey("extraLife") == false)
            {
                PurchasedProductExtraLife.Invoke();

                FirebaseAnalytics.LogEvent(name: "extra_life_purchased");
            }           
        }
        else if (String.Equals(product.definition.id, moneyPack2000, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", product.definition.id));

            PurchasedProductMoneyPack2000.Invoke();

            FirebaseAnalytics.LogEvent(name: "money_pack_2k_purchased");
        }
        else if (String.Equals(product.definition.id, moneyPack5000, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", product.definition.id));

            PurchasedProductMoneyPack5000.Invoke();

            FirebaseAnalytics.LogEvent(name: "money_pack_5k_purchased");
        }
        else if (String.Equals(product.definition.id, moneyPack10000, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", product.definition.id));

            PurchasedProductMoneyPack10000.Invoke();

            FirebaseAnalytics.LogEvent(name: "money_pack_10k_purchased");
        }
        else if (String.Equals(product.definition.id, powerRestore, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", product.definition.id));

            PurchasedProductPowersToRestore.Invoke();

            FirebaseAnalytics.LogEvent(name: "powers_restore_purchased");
        }
        else if (String.Equals(product.definition.id, moneyPack5000ForPreRegistration, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", product.definition.id));

            Product_MoneyPack5000ForPreRegistration();
        }
        else if (String.Equals(product.definition.id, specialOfferEnergyRestore, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", product.definition.id));

            PurchasedSpecialOfferEnergyRecovery.Invoke();
        }
        else if (String.Equals(product.definition.id, specialOfferMoneyPack3500, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", product.definition.id));

            PurchasedSpecialOfferMoneyPack3500.Invoke();
        }
        else if (String.Equals(product.definition.id, specialOfferStarterPack, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", product.definition.id));

            PurchasedSpecialOfferStarterPack.Invoke();
        }
        else if (String.Equals(product.definition.id, extraLifeSpecial, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", product.definition.id));

            if (PlayerPrefs.HasKey("extraLife") == false)
            {
                PurchasedProductExtraLifeSpecial.Invoke();

                FirebaseAnalytics.LogEvent(name: "extra_life_special_purchased");
            }
        }
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", product.definition.id));
        }

        return PurchaseProcessingResult.Complete;
    }
    #endregion

    #region PRE-REGISTRATION BONUS   
    private void Product_MoneyPack5000ForPreRegistration()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        TotalScore = TotalScore + 5000;
        Debug.Log("!!!--- TotalScore + MoneyPack 5000 For Pre Registration ---!!! " + TotalScore);
        SoundManager.snd.PlaybuySounds();
        PlayerPrefs.SetInt("TotalScore", TotalScore);

        FirebaseAnalytics.LogEvent(name: "bonus_received");
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

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log($"In-App Purchasing initialize failed: {error}, Message: {message}");
    }

    public void OnPurchaseComplete(Product product)
    {
        Debug.Log($"Purchase completed - Product: '{product.definition.id}'");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
    }

}