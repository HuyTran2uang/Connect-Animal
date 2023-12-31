﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Purchasing;

public class ProductNoAdsPackageUI : ProductPackageUI, IBoughtRemoveAds
{
    [SerializeField] TMP_Text _priceText;

    public void Init(string id, ProductMetadata metadata)
    {
        _id = id;
        string price = metadata?.localizedPriceString;
        price = price.Replace("₫", "VND");
        _priceText.text = price;
    }

    public void BoughtRemoveAds()
    {
        _buyButton.interactable = false;
    }
}
