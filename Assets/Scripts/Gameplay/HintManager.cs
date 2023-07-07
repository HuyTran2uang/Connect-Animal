using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviourSingleton<HintManager>
{
    private bool _isHint;

    public void Hint()
    {
        if (_isHint) return;
        _isHint = true;
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
}
