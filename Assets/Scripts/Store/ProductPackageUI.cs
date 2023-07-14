using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProductPackageUI : PackageUI
{
    protected string _id;

    protected override void OnBuyPackage()
    {
        base.OnBuyPackage();
        IAPManager.Instance.BuyProductID(_id);
    }
}
