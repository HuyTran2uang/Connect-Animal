using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BombManager : MonoBehaviourSingleton<BombManager>, IReadData, IPrepareGame
{
    [SerializeField] Bomb _bomPrefab;
    private bool _isThrowBomb;
    [SerializeField] private int _numBomb = 2, _countBomb, _totalThrowBombTimes;

    public void LoadData()
    {
        _totalThrowBombTimes = Data.ReadData.LoadData(GlobalKey.TOTAL_BOMB_TIMES, 10);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
    }

    public void Prepare()
    {
        BombUI.Instance.ChangeQuantity(_totalThrowBombTimes);
    }

    public void Throw()
    {
        if (GameManager.Instance.GameState != GameState.OnBattle) return;
        if (_totalThrowBombTimes == 0) return;
        if (_isThrowBomb) return;
        _isThrowBomb = true;
        _totalThrowBombTimes--;
        Data.WriteData.Save(GlobalKey.TOTAL_BOMB_TIMES, _totalThrowBombTimes);
        BombUI.Instance.ChangeQuantity(_totalThrowBombTimes);
        _countBomb = _numBomb;
        Couple couple = BoardManager.Instance.GetRandomCouple();
        if(couple == null)
        {
            Debug.Log("No Exist Couple");
            return;
        }
        Bomb bomb1 = Instantiate(_bomPrefab, BombSpawner.Instance.transform.position,  Quaternion.identity);
        bomb1.Target(couple.Coord1.x, couple.Coord1.y);
        Bomb bomb2 = Instantiate(_bomPrefab, BombSpawner.Instance.transform.position, Quaternion.identity);
        bomb2.Target(couple.Coord2.x, couple.Coord2.y);
    }

    public void CompletedThrowBomb()
    {
        BoardManager.Instance.ResetDataMap();
        _isThrowBomb = false;
        if (!BoardManager.Instance.CheckCompletedMap()) return;
        GameManager.Instance.Win();
    }

    public void OnBombExplode(Bomb bomb)
    {
        bomb.Explode();
        Destroy(gameObject);
        _countBomb--;
        if (_countBomb > 0) return;
        CompletedThrowBomb();
    }

    public void AddThrowTimes(int quantity)
    {
        _totalThrowBombTimes += quantity;
        Data.WriteData.Save(GlobalKey.TOTAL_BOMB_TIMES, _totalThrowBombTimes);
        BombUI.Instance.ChangeQuantity(_totalThrowBombTimes);
    }
}
