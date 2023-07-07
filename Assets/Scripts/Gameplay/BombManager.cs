using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviourSingleton<BombManager>, IReadData, IPrepareGame
{
    [SerializeField] Bomb _bomPrefab;
    private bool _isThrowBomb;
    private int _numBomb = 2, _countBomb, _totalThrowBombTimes;

    public void LoadData()
    {
        _totalThrowBombTimes = Data.ReadData.LoadData(GlobalKey.TOTAL_BOMB_TIMES, 0);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
    }

    public void Prepare()
    {
        BombUI.Instance.ChangeQuantity(_totalThrowBombTimes);
    }

    public void Throw()
    {
        if (_isThrowBomb) return;
        _isThrowBomb = true;
        _totalThrowBombTimes--;
        Data.WriteData.Save(GlobalKey.TOTAL_BOMB_TIMES, _totalThrowBombTimes);
        BombUI.Instance.ChangeQuantity(_totalThrowBombTimes);
        _countBomb = _numBomb;
        Couple couple = BoardManager.Instance.GetRandomCouple();
        Bomb bomb1 = Instantiate(_bomPrefab, transform);
        bomb1.Target(couple.Coord1.x, couple.Coord1.y);
        Bomb bomb2 = Instantiate(_bomPrefab, transform);
        bomb2.Target(couple.Coord2.x, couple.Coord2.y);
    }

    public void CompletedThrowBomb()
    {
        _isThrowBomb = false;
    }

    public void OnBombExplode(Bomb bomb)
    {
        _countBomb--;
        if(_countBomb == 0)
        {
            CompletedThrowBomb();
        }
    }

    public void AddThrowTimes(int quantity)
    {
        _totalThrowBombTimes += quantity;
        Data.WriteData.Save(GlobalKey.TOTAL_BOMB_TIMES, _totalThrowBombTimes);
        BombUI.Instance.ChangeQuantity(_totalThrowBombTimes);
    }
}
