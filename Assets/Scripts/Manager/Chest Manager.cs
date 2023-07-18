using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviourSingleton<ChestManager>, IPrepareGame
{
    LevelChestPanel _levelChestPanel;
    public void Prepare()
    {
        _levelChestPanel = FindObjectOfType<LevelChestPanel>(true);
    }

    public void OpenLevelChest()
    {
        _levelChestPanel.gameObject.SetActive(true);
    }
}
