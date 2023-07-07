using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelChestPanel : MonoBehaviour
{
    [SerializeField] List<Key> keys;
    [SerializeField] List<LevelChest> levelChests;

    [SerializeField] GameObject boardKey;
    [SerializeField] Button backButton, watchAdsButton;
    [SerializeField] TMP_Text coinText;

    int coin;
    int countKey;
    int countKeyLimit = 3;

    public void CheckOpenedChest()
    {
        foreach (LevelChest levelChest in levelChests)
        {
            if (levelChest.isOpened)
            {
                countKey++;
                if (countKey == countKeyLimit)
                {
                    for (int i = 0; i < levelChests.Count; i++)
                    {
                        levelChests[i].SetDeActive();
                    }
                    ActiveWatchADS();
                }
                else
                {
                    int index = keys.Count - countKey;
                    if (index < 0)
                    {
                        index = 0;
                    }
                    keys[index].UnKey();
                }
            }
        }
        countKey = 0;
    }
    public void ActiveWatchADS()
    {
        boardKey.SetActive(false);
        watchAdsButton.gameObject.SetActive(true);
    }

    public void Update()
    {
        CheckOpenedChest();
    }
}
