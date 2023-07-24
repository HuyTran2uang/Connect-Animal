using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviourSingleton<ChestManager>
{
    [SerializeField] LevelChestPanel _levelChestPanel;
    [SerializeField] StarChestPopupUI _starChestPopupUI;
    int _countKey, _countBuyKey;

    public int CountKey => _countKey;
    public int CountBuyKey => _countBuyKey;

    public void OpenChest(Chest chest)
    {
        chest.Open();
        _countKey--;
        if (_countKey < 0) return;
        _starChestPopupUI.UnKey(_countKey);
        _levelChestPanel.UnKey(_countKey);
        Debug.Log(_countKey);
        if(_countKey == 0 && _countBuyKey == 0)
        {
            _levelChestPanel.ShowWatchAdsButton();
            _starChestPopupUI.ShowWatchAdsButton();
        } 
    }

    public void ShowStarChests()
    {
        _countKey = 3;
        _countBuyKey = 0;
        _starChestPopupUI.gameObject.SetActive(true);
    }

    public void ShowLevelChest()
    {
        _countKey = 3;
        _countBuyKey = 0;
        _levelChestPanel.gameObject.SetActive(true);
    }

    public void BuyKey()
    {
        _countBuyKey++;
        _countKey = 3;
        _levelChestPanel.HideWatchAdsButton();
        _starChestPopupUI.HideWatchAdsButton();
    }
}
