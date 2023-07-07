using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    [SerializeField] Bomb _bomPrefab;
    private bool _isThrowBomb;
    private int _numBomb = 2, _countBomb;

    public void Throw()
    {
        if (_isThrowBomb) return;
        _isThrowBomb = true;
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
}
