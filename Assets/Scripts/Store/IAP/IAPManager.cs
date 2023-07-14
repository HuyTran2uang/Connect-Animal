using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.SceneManagement;

public class IAPManager : MonoBehaviourSingleton<IAPManager>, IDetailedStoreListener, IPrepareGame
{
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;
    private Dictionary<string, ProductData> _productDataDict = new Dictionary<string, ProductData>();

    //ids
    const string IAP_BIG_PACK_1 = "iap_bigpack_1";
    const string IAP_BIG_PACK_2 = "iap_bigpack_2";
    const string IAP_BIG_PACK_3 = "iap_bigpack_3";
    const string IAP_PACK_1 = "iap_pack_1";
    const string IAP_PACK_2 = "iap_pack_2";
    const string IAP_PACK_3 = "iap_pack_3";
    const string IAP_REMOVE_ADS = "iap_remove_ads";
    //

    public void Prepare()
    {
        Init();
    }

    private void Init()
    {
        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }
    }

    internal ProductMetadata GetMetaData(string id)
    {
        return m_StoreController.products.WithID(id).metadata;
    }

    private void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }

        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        _productDataDict.Add(IAP_BIG_PACK_1, FactoryProductData.CreateProductBigPackage(IAP_BIG_PACK_1, ProductType.Consumable, 1000, 5, 5, 5, 3));
        _productDataDict.Add(IAP_BIG_PACK_2, FactoryProductData.CreateProductBigPackage(IAP_BIG_PACK_2, ProductType.Consumable, 3000, 15, 15, 15, 9));
        _productDataDict.Add(IAP_BIG_PACK_3, FactoryProductData.CreateProductBigPackage(IAP_BIG_PACK_3, ProductType.Consumable, 10000, 40, 40, 40, 21));
        _productDataDict.Add(IAP_PACK_1, FactoryProductData.CreateProductPakageCoin(IAP_PACK_3, ProductType.Consumable, 2000));
        _productDataDict.Add(IAP_PACK_2, FactoryProductData.CreateProductPakageCoin(IAP_PACK_3, ProductType.Consumable, 6000));
        _productDataDict.Add(IAP_PACK_3, FactoryProductData.CreateProductPakageCoin(IAP_PACK_3, ProductType.Consumable, 14000));
        _productDataDict.Add(IAP_REMOVE_ADS, FactoryProductData.CreateProductPackageNoAds(IAP_REMOVE_ADS, ProductType.Consumable));

        foreach (var productData in _productDataDict.Values)
        {
            builder.AddProduct(productData.Id, productData.ProductType);   
        }

        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    public void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }


    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    //public void RestorePurchases()
    //{
    //    // If Purchasing has not yet been set up ...
    //    if (!IsInitialized())
    //    {
    //        // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
    //        Debug.Log("RestorePurchases FAIL. Not initialized.");
    //        return;
    //    }

    //    // If we are running on an Apple device ... 
    //    if (Application.platform == RuntimePlatform.IPhonePlayer ||
    //        Application.platform == RuntimePlatform.OSXPlayer)
    //    {
    //        // ... begin restoring purchases
    //        Debug.Log("RestorePurchases started ...");

    //        // Fetch the Apple store-specific subsystem.
    //        var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
    //        // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
    //        // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
    //        apple.RestoreTransactions((result) =>
    //        {
    //            // The first phase of restoration. If no more responses are received on ProcessPurchase then 
    //            // no purchases are available to be restored.
    //            Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
    //        });
    //    }
    //    // Otherwise ...
    //    else
    //    {
    //        // We are not running on an Apple device. No work is necessary to restore purchases.
    //        Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
    //    }
    //}


    //  
    // --- IStoreListener
    //

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error + "\n" + message);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {

    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        _productDataDict[args.purchasedProduct.definition.id].Package.GetReward();

        // Return a flag indicating whether this product has completely been received, or if the application needs 
        // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
        // saving purchased products to the cloud, and when that save is delayed. 
        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}

public class ProductData
{
    private string _id;
    private ProductType _productType;
    private IPackage _package;
    private ProductMetadata _metaData;

    public ProductData(string id, ProductType productType, IPackage package)
    {
        _id = id;
        _productType = productType;
        _package = package;
        _metaData = IAPManager.Instance.GetMetaData(_id);
    }

    public string Id => _id;
    public ProductType ProductType => _productType;
    public IPackage Package => _package;
    public ProductMetadata Metadata => _metaData;
}
