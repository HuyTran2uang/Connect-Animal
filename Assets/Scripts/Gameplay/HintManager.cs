using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviourSingleton<HintManager>, IReadData, IPrepareGame
{
    private int _totalHintTimes;
    private bool _isHint;

    public void LoadData()
    {
        _totalHintTimes = Data.ReadData.LoadData(GlobalKey.TOTAL_HINT_TIMES, 3);
    }

    public void Prepare()
    {
        HintUI.Instance.ChangeQuantity(_totalHintTimes);
    }

    public void Hint()
    {
        if (_totalHintTimes == 0) return;
        if (_isHint) return;
        _isHint = true;
        _totalHintTimes--;
        Data.WriteData.Save(GlobalKey.TOTAL_HINT_TIMES, _totalHintTimes);
        HintUI.Instance.ChangeQuantity(_totalHintTimes);
        Couple couple = BoardManager.Instance.GetGraphFirst().GetGraphKeyFirst();
        BoardManager.Instance.BoardUI[couple.Coord1.x, couple.Coord1.y].Hint();
        BoardManager.Instance.BoardUI[couple.Coord2.x, couple.Coord2.y].Hint();
    }

    public void UnHint()
    {
        if (!_isHint) return;
        _isHint = false;
        ItemSpawner.Instance.UnHint();
    }

    public void AddHintTimes(int quantity)
    {
        _totalHintTimes += quantity;
        Data.WriteData.Save(GlobalKey.TOTAL_HINT_TIMES, _totalHintTimes);
        HintUI.Instance.ChangeQuantity(_totalHintTimes);
    }
}
