using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Purchasing;

public class PreRegistrationBonus : MonoBehaviour, IStoreListener
{
    private static IStoreController _storeController;
    private static IExtensionProvider _extensionProvider;

    private string noAds = "com.catastrophe.noads";
    private string extraLife = "com.catastrophe.extralife";
    private string moneyPack2000 = "com.catastrophe.moneypack2000";
    private string moneyPack5000 = "com.catastrophe.moneypack5k";
    private string moneyPack10000 = "com.catastrophe.moneypack10000";
    private string powerRestore = "com.catastrophe.powerstorestore";

    private string moneyPack5000ForPreRegistration = "com.catastrophe.moneypack5000";

    private int TotalScore;

    void Start()
    {
        if (_storeController == null)
        {
            InitializePurchasing();
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

        UnityPurchasing.Initialize(this, builder);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        var product = args.purchasedProduct;

        if (String.Equals(product.definition.id, moneyPack5000ForPreRegistration, StringComparison.Ordinal))
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

    private void Product_MoneyPack5000ForPreRegistration()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        TotalScore = TotalScore + 5000;
        SoundManager.snd.PlaybuySounds();
        PlayerPrefs.SetInt("TotalScore", TotalScore);
    }

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

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
    }
}
