using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BombManager : MonoBehaviourSingleton<BombManager>, IReadData, IPrepareGame
{
    [SerializeField] Bomb _bomPrefab;
    private bool _isThrowingBomb;
    [SerializeField] private int _numBomb = 2, _countBomb, _totalThrowBombTimes;
    List<IBombText> _bombTexts = new List<IBombText>();

    public void LoadData()
    {
        _totalThrowBombTimes = Data.ReadData.LoadData(GlobalKey.TOTAL_BOMB_TIMES, 3);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
    }

    public void Prepare()
    {
        BombUI.Instance.ChangeQuantity(_totalThrowBombTimes);
        _bombTexts = FindObjectsOfType<MonoBehaviour>(true).OfType<IBombText>().ToList();
        _bombTexts.ForEach(i => i.SetQuantityText(_totalThrowBombTimes));
    }

    public void Throw()
    {
        if (_isThrowingBomb) return;
        if (_totalThrowBombTimes == 0)
        {
            if(CurrencyManager.Instance.SubtractCoint(150))
            {
                _totalThrowBombTimes++;
            }
            else
            {
                return;       
            }
        }
        _isThrowingBomb = true;
        _totalThrowBombTimes--;
        Data.WriteData.Save(GlobalKey.TOTAL_BOMB_TIMES, _totalThrowBombTimes);
        BombUI.Instance.ChangeQuantity(_totalThrowBombTimes);
        _bombTexts.ForEach(i => i.SetQuantityText(_totalThrowBombTimes));
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
        _isThrowingBomb = false;
        if (!BoardManager.Instance.CheckCompletedMap()) return;
        GameManager.Instance.Win();
    }

    public void OnBombExplode(Bomb bomb)
    {
        bomb.Explode();
        Destroy(bomb.gameObject);
        _countBomb--;
        if (_countBomb > 0) return;
        CompletedThrowBomb();
    }

    public void AddThrowTimes(int quantity)
    {
        _totalThrowBombTimes += quantity;
        Data.WriteData.Save(GlobalKey.TOTAL_BOMB_TIMES, _totalThrowBombTimes);
        BombUI.Instance.ChangeQuantity(_totalThrowBombTimes);
        _bombTexts.ForEach(i => i.SetQuantityText(_totalThrowBombTimes));
    }
}
