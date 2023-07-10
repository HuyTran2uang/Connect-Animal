using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviourSingleton<MapManager>, IReadData, IPrepareGame
{
    private int _totalRemapTimes;

    public void LoadData()
    {
        _totalRemapTimes = Data.ReadData.LoadData(GlobalKey.TOTAL_REMAP_TIMES, 9);
    }

    public void Prepare()
    {
        RemapUI.Instance.ChangeQuantity(_totalRemapTimes);
    }

    public void Remap()
    {
        if (_totalRemapTimes == 0) return;
        _totalRemapTimes--;
        BoardManager.Instance.Remap();
        Data.WriteData.Save(GlobalKey.TOTAL_REMAP_TIMES, _totalRemapTimes);
        RemapUI.Instance.ChangeQuantity(_totalRemapTimes);
    }

    public void AddRemapTimes(int quantity)
    {
        _totalRemapTimes += quantity;
        Data.WriteData.Save(GlobalKey.TOTAL_REMAP_TIMES, _totalRemapTimes);
        RemapUI.Instance.ChangeQuantity(_totalRemapTimes);
    }
}
