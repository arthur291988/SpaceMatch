using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManagerOfGame : MonoBehaviour, IStoreListener
{
    IStoreController m_StoreController; // The Unity Purchasing system.
    IExtensionProvider m_StoreExtensionProvider; // The Unity Purchasing system.

    [HideInInspector]
    public string NO_ADS = "NO_ADS";
    [HideInInspector]
    public string NO_ADS_SPECIAL = "NO_ADS_SPECIAL";

    public static IAPManagerOfGame instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InitializePurchasing();
    }

    void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(NO_ADS, ProductType.NonConsumable);
        builder.AddProduct(NO_ADS_SPECIAL, ProductType.NonConsumable);

        UnityPurchasing.Initialize(this, builder);
    }



    // Example method called when the user presses a 'buy' button
    // to start the purchase process.
    public void BuyNOAds()
    {
        m_StoreController.InitiatePurchase(NO_ADS);
    }
    public void BuyNOAdsSpecial()
    {
        m_StoreController.InitiatePurchase(NO_ADS_SPECIAL);
    }
    public bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("In-App Purchasing successfully initialized");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        //Retrieve the purchased product
        var product = args.purchasedProduct;

        //Add the purchased product to the players inventory
        if (product.definition.id == NO_ADS)
        {
            IAPManager.instance.buyNoAdsFromMenu();
        }
        else if (product.definition.id == NO_ADS_SPECIAL)
        {
            IAPManager.instance.buyNoAdsSpecial();
        }

        Debug.Log($"Purchase Complete - Product: {product.definition.id}");

        //We return Complete, informing IAP that the processing on our side is done and the transaction can be closed.
        return PurchaseProcessingResult.Complete;
    }
    public string getProductPriceFromStore(string id)
    {
        if (m_StoreController != null && m_StoreController.products != null) return m_StoreController.products.WithID(id).metadata.localizedPriceString;
        else return "";
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        var errorMessage = $"Purchasing failed to initialize. Reason: {error}.";

        Debug.Log(errorMessage);
        throw new NotImplementedException();
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new NotImplementedException();
    }
}
