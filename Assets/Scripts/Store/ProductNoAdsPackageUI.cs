using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Purchasing;

public class ProductNoAdsPackageUI : ProductPackageUI
{
    [SerializeField] TMP_Text _priceText;

    public void Init(string id, ProductMetadata metadata)
    {
        _id = id;
        _priceText.text = metadata.localizedPriceString;
    }
}
