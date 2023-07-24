using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using TMPro;

public class ProductBigPackageUI : ProductPackageUI
{
    [SerializeField] List<GameObject> _iconChestes = new List<GameObject>();
    [SerializeField] TMP_Text _priceText, _coinText, _bombText, _hintText, _remapText, _daysText;

    public void Init(string id, BigPackage package, ProductMetadata metadata)
    {
        _id = id;
        switch (_id)
        {
            case "iap_bigpack_1":
                _iconChestes[0].SetActive(true);
                break;
            case "iap_bigpack_2":
                _iconChestes[1].SetActive(true);
                break;
            case "iap_bigpack_3":
                _iconChestes[2].SetActive(true);
                break;
        }
        string price = metadata?.localizedPriceString;
        price = price.Replace("₫", "VND");
        _priceText.text = price;
        _coinText.text = package.Coin.ToString();
        _bombText.text = package.Bomb.ToString();
        _hintText.text = package.Hint.ToString();
        _remapText.text = package.Remap.ToString();
        _daysText.text = $"NO ADS {package.QuantityDayNoAds} DAYS";
    }
}
