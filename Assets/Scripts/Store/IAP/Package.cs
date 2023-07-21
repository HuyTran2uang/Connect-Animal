using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPackage
{
    void GetReward();
}

public class PackageCoin : IPackage
{
    private int _coin;

    public PackageCoin(int coint)
    {
        _coin = coint;
    }

    public void GetReward()
    {
        CurrencyManager.Instance.AddCoint(_coin);
    }

    public int Coin => _coin;
}

public class PackageNoAds : IPackage
{
    public PackageNoAds() { }

    public void GetReward()
    {
        ApplovinManager.Instance.NoAds();
    }
}

public class BigPackage : IPackage
{
    private int _coin, _bomb, _hint, _remap, _quantityDayNoAds;

    public BigPackage(int coin, int bomb, int hint, int remap, int quantityDayNoAds)
    {
        _coin = coin;
        _bomb = bomb;
        _hint = hint;
        _remap = remap;
        _quantityDayNoAds = quantityDayNoAds;
    }

    public void GetReward()
    {
        CurrencyManager.Instance.AddCoint(_coin);
        BombManager.Instance.AddThrowTimes(_bomb);
        HintManager.Instance.AddHintTimes(_hint);
        RemapManager.Instance.AddRemapTimes(_remap);
        //_quantityDayNoAds
    }

    public int Coin => _coin;
    public int Bomb => _bomb;
    public int Hint => _hint;
    public int Remap => _remap;
    public int QuantityDayNoAds => _quantityDayNoAds;
}
