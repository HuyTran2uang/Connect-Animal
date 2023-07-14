using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PakageCoinSpawner : MonoBehaviour
{
    [SerializeField] List<PackageCoin> _packageCoins = new List<PackageCoin>();

    public void Add(PackageCoin packageCoin)
    {
        _packageCoins.Add(packageCoin);
    }
}
