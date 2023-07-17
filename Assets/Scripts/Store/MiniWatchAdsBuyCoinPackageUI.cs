using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniWatchAdsBuyCoinPackageUI : WatchAdsBuyCoinPackageUI
{
    public override void Init(PackageCoin package)
    {
        _package = package;
        _coinText.text = $"+{_package.Coin}";
    }
}
