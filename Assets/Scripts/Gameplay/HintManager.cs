using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HintManager : MonoBehaviourSingleton<HintManager>, IReadData, IPrepareGame
{
    private int _totalHintTimes;
    private bool _isHint;
    List<IHintText> _hintTexts = new List<IHintText>();

    public void LoadData()
    {
        _totalHintTimes = Data.ReadData.LoadData(GlobalKey.TOTAL_HINT_TIMES, 3);
    }

    public void Prepare()
    {
        HintUI.Instance.ChangeQuantity(_totalHintTimes);
        _hintTexts = FindObjectsOfType<MonoBehaviour>(true).OfType<IHintText>().ToList();
        _hintTexts.ForEach(i => i.SetQuantityText(_totalHintTimes));
    }

    public bool Hint()
    {
        if (_totalHintTimes == 0) return false;
        if (_isHint) return false;
        _isHint = true;
        _totalHintTimes--;
        Data.WriteData.Save(GlobalKey.TOTAL_HINT_TIMES, _totalHintTimes);
        HintUI.Instance.ChangeQuantity(_totalHintTimes);
        _hintTexts.ForEach(i => i.SetQuantityText(_totalHintTimes));
        Couple couple = BoardManager.Instance.GetFirstGraphExistCouple().GetGraphKeyFirst();
        BoardManager.Instance.BoardUI[couple.Coord1.x, couple.Coord1.y].Hint();
        BoardManager.Instance.BoardUI[couple.Coord2.x, couple.Coord2.y].Hint();
        return true;
    }

    public void HintFree()
    {
        if (LevelManager.Instance.Level > 5) return;
        if (_isHint) return;
        _isHint = true;
        Data.WriteData.Save(GlobalKey.TOTAL_HINT_TIMES, _totalHintTimes);
        HintUI.Instance.ChangeQuantity(_totalHintTimes);
        _hintTexts.ForEach(i => i.SetQuantityText(_totalHintTimes));
        Couple couple = BoardManager.Instance.GetFirstGraphExistCouple().GetGraphKeyFirst();
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
        _hintTexts.ForEach(i => i.SetQuantityText(_totalHintTimes));
    }
}
