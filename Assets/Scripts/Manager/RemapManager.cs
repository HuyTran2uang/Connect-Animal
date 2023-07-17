using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RemapManager : MonoBehaviourSingleton<RemapManager>, IReadData, IPrepareGame
{
    private int _totalRemapTimes;
    List<IRemapText> _remapTexts = new List<IRemapText>();

    public void LoadData()
    {
        _totalRemapTimes = Data.ReadData.LoadData(GlobalKey.TOTAL_REMAP_TIMES, 3);
    }

    public void Prepare()
    {
        RemapUI.Instance.ChangeQuantity(_totalRemapTimes);
        _remapTexts = FindObjectsOfType<MonoBehaviour>(true).OfType<IRemapText>().ToList();
        _remapTexts.ForEach(i => i.SetQuantityText(_totalRemapTimes));
    }

    public bool Remap()
    {
        if (_totalRemapTimes == 0) return false;
        _totalRemapTimes--;
        BoardManager.Instance.Remap();
        Data.WriteData.Save(GlobalKey.TOTAL_REMAP_TIMES, _totalRemapTimes);
        RemapUI.Instance.ChangeQuantity(_totalRemapTimes);
        _remapTexts.ForEach(i => i.SetQuantityText(_totalRemapTimes));
        return true;
    }

    public void AddRemapTimes(int quantity)
    {
        _totalRemapTimes += quantity;
        Data.WriteData.Save(GlobalKey.TOTAL_REMAP_TIMES, _totalRemapTimes);
        RemapUI.Instance.ChangeQuantity(_totalRemapTimes);
        _remapTexts.ForEach(i => i.SetQuantityText(_totalRemapTimes));
    }
}
