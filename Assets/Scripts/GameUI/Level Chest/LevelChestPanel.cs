using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Pool;
using System;

public class LevelChestPanel : MonoBehaviourSingleton<LevelChestPanel>
{
    [SerializeField] List<Key> keys;
    [SerializeField] List<LevelChest> levelChests;

    [SerializeField] GameObject levelChestPopup;
    [SerializeField] GameObject boardKey;
    [SerializeField] Button backButton, watchAdsButton;
    [SerializeField] TMP_Text coinText;

    int coin;
    int countKey = 3;   

    public int CountKey => countKey;

    public void CheckOpenedChest()
    {
        countKey = 3;
        foreach (LevelChest levelChest in levelChests)
        {
            if (levelChest.isOpened)
            {
                countKey --;
                if (countKey <= 0)
                {
                    ActiveWatchADS();
                }
            }
        }
    }

    private void Awake()
    {
        backButton.onClick.AddListener(Back);
        watchAdsButton.onClick.AddListener(WatchADSDone);
        foreach (LevelChest levelChest in levelChests)
        {
            levelChest.ButtonChest.onClick.AddListener(CheckOpenedChest);
        }
    }

    public void WatchADSDone()
    {
        countKey = 9;
    }
    public void Back()
    {
        levelChestPopup.SetActive(false);
    }
    public void ActiveWatchADS()
    {
        boardKey.SetActive(false);
        watchAdsButton.gameObject.SetActive(true);
    }
    private void OnEnable()
    {
        foreach (LevelChest levelChest in levelChests)
        {
            levelChest.ResetChest();
        }
    }
}
