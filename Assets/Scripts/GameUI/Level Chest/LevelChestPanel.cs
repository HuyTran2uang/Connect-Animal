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
    [SerializeField] List<Chest> chests;

    [SerializeField] GameObject boardKey;
    [SerializeField] Button backButton, watchAdsButton;

    private void Awake()
    {
        backButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            gameObject.SetActive(false);
        });

        watchAdsButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            ApplovinManager.Instance.ShowRewardedAd(delegate
            {
                ChestManager.Instance.BuyKey();
                ResetKey();
            });
        });
    }

    public void ShowWatchAdsButton()
    {
        boardKey.SetActive(false);
        watchAdsButton.gameObject.SetActive(true);
    }

    public void HideWatchAdsButton()
    {
        boardKey.SetActive(true);
        watchAdsButton.gameObject.SetActive(false);
    }
    public void UnKey(int index)
    {
        keys[index].UnKey();
    }

    public void ResetKey()
    {
        foreach (Key key in keys)
        {
            key.ActiveKey();
        }
    }

    private void OnEnable()
    {
        ResetKey();
        ChestManager.Instance.ShowLevelChest();
        foreach (Chest chest in chests)
        {
            chest.ResetChest();
        }
    }
}
