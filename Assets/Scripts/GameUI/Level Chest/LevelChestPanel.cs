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
        backButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            Back();
        });

        watchAdsButton.onClick.AddListener(delegate
        {
            ApplovinManager.Instance.ShowRewardedAd(delegate
            {
                WatchADSDone();
            });
        });

        foreach (LevelChest levelChest in levelChests)
        {
            levelChest.ButtonChest.onClick.AddListener(delegate
            {
                if (countKey == 2)
                {
                    ApplovinManager.Instance.ShowRewardedAd(delegate
                    {
                        AudioManager.Instance.PlaySoundClickButton();
                        CheckOpenedChest();
                    });
                }
                else
                {
                    AudioManager.Instance.PlaySoundClickButton();
                    CheckOpenedChest();
                }
            });
        }
    }

    public void WatchADSDone()
    {
        countKey = 9;
    }
    public void Back()
    {
        levelChestPopup.SetActive(false);
        GameManager.Instance.Continue();
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
