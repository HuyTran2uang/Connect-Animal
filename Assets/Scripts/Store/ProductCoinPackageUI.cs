using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Purchasing;

public class ProductCoinPackageUI : ProductPackageUI
{
    [SerializeField] List<GameObject> _icons = new List<GameObject>();
    [SerializeField] TMP_Text _coinText, _priceText;

    public void Init(string id, PackageCoin package, ProductMetadata metadata)
    {
        _id = id;
        switch (_id)
        {
            case "iap_pack_1":
                _icons[0].SetActive(true);
                break;
            case "iap_pack_2":
                _icons[1].SetActive(true);
                break;
            case "iap_pack_3":
                _icons[2].SetActive(true);
                break;
        }
        _coinText.text = package.Coin.ToString();
        if (_priceText != null)
        {
            string price = metadata?.localizedPriceString;
            price = price.Replace("₫", "VND");
            _priceText.text = price;
        }
        else
            _coinText.text = $"+{package.Coin}";
    }
}
