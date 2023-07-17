using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class FactoryProductData
{
    public static ProductData CreateProductPakageCoin(string id, ProductType productType, int coin)
    {
        PackageCoin package = new PackageCoin(coin);
        ProductData product = new ProductData(id, productType, package);
        PackageUISpawner.Instance.SpawnProductCoinPackageUI(id, package, product.Metadata);
        return product;
    }

    public static ProductData CreateProductPackageNoAds(string id, ProductType productType)
    {
        PackageNoAds package = new PackageNoAds();
        ProductData product = new ProductData(id, productType, package);
        PackageUISpawner.Instance.SpawnProductNoAdsPackageUI(id, package, product.Metadata);
        return product;
    }

    public static ProductData CreateProductBigPackage(string id, ProductType productType, int coin, int bomb, int hint, int remap, int quantityDaysNoAds)
    {
        BigPackage package = new BigPackage(coin, bomb, hint, remap, quantityDaysNoAds);
        ProductData product = new ProductData(id, productType, package);
        PackageUISpawner.Instance.SpawnProductBigPackageUI(id, package, product.Metadata);
        return product;
    }
}

public class FactoryPackageWatchAds
{
    public static void CreatePackageCoin(int coin)
    {
        PackageCoin package = new PackageCoin(coin);
        PackageUISpawner.Instance.SpawnWatchAdsBuyCoinPackageUI(package);
    }
}