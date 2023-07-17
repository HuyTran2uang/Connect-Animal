using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class PackageUISpawner : MonoBehaviourSingleton<PackageUISpawner>
{
    [SerializeField] ProductBigPackageUI _productBigPackageUI;
    [SerializeField] ProductCoinPackageUI _productCoinPackageUI;
    [SerializeField] ProductNoAdsPackageUI _productNoAdsPackageUI;
    [SerializeField] WatchAdsBuyCoinPackageUI _watchAdsBuyCoinPackageUI;
    [SerializeField] Transform _storeContainer;
    [Header("Not Enough Coin Popup")]
    [SerializeField] ProductCoinPackageUI _miniProdudctCoinPackageUI;
    [SerializeField] WatchAdsBuyCoinPackageUI _miniWatchAdsBuyCoinPackageUI;
    [SerializeField] Transform _packCoinContainer, _watchAdsContainer;

    public void SpawnProductBigPackageUI(string id, BigPackage bigPackage, ProductMetadata metadata)
    {
        ProductBigPackageUI productBigPackageUI = Instantiate(_productBigPackageUI, _storeContainer);
        productBigPackageUI.Init(id, bigPackage, metadata);
    }

    public void SpawnProductCoinPackageUI(string id, PackageCoin packageCoin, ProductMetadata metadata)
    {
        ProductCoinPackageUI productCoinPackageUI = Instantiate(_productCoinPackageUI, _storeContainer);
        productCoinPackageUI.Init(id, packageCoin, metadata);
        ProductCoinPackageUI miniWatchAdsBuyCoinPackageUI = Instantiate(_miniProdudctCoinPackageUI, _packCoinContainer);
        miniWatchAdsBuyCoinPackageUI.Init(id, packageCoin, metadata);
    }

    public void SpawnProductNoAdsPackageUI(string id, PackageNoAds packageNoAds, ProductMetadata metadata)
    {
        ProductNoAdsPackageUI productNoAdsPackageUI = Instantiate(_productNoAdsPackageUI, _storeContainer);
        productNoAdsPackageUI.Init(id, metadata);
    }

    public void SpawnWatchAdsBuyCoinPackageUI(PackageCoin packageCoin)
    {
        WatchAdsBuyCoinPackageUI watchAdsBuyCoinPackageUI = Instantiate(_watchAdsBuyCoinPackageUI, _storeContainer);
        watchAdsBuyCoinPackageUI.Init(packageCoin);
        WatchAdsBuyCoinPackageUI miniwatchAdsBuyCoinPackageUI = Instantiate(_miniWatchAdsBuyCoinPackageUI, _watchAdsContainer);
        miniwatchAdsBuyCoinPackageUI.Init(packageCoin);
    }
}
