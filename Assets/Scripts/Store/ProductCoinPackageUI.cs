using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Purchasing;

public class ProductCoinPackageUI : ProductPackageUI
{
    [SerializeField] TMP_Text _coinText, _priceText;

    public void Init(string id, PackageCoin package, ProductMetadata metadata)
    {
        _id = id;
        _coinText.text = package.Coin.ToString();
        _priceText.text = metadata.localizedPriceString;
    }
}
