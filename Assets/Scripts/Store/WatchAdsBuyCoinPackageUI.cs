using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WatchAdsBuyCoinPackageUI : PackageUI
{
    [SerializeField] protected TMP_Text _coinText;
    protected PackageCoin _package;

    protected override void OnBuyPackage()
    {
        base.OnBuyPackage();
        ApplovinManager.Instance.ShowRewardedAd(delegate
        {
            _package.GetReward();
        });
    }

    public virtual void Init(PackageCoin package)
    {
        _package = package;
        _coinText.text = _package.Coin.ToString();
    }
}
