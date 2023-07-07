using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    int _row, _col;

    public void Target(int row, int col)
    {
        _row = row;
        _col = col;
        Vector3 targetPos = BoardManager.Instance.Board[_row, _col].Pos;
        MoveToTarget(targetPos);
    }

    private void MoveToTarget(Vector3 targetPos)
    {
        transform.DOMove(targetPos, .5f).OnComplete(MoveCompleted);
    }

    private void MoveCompleted()
    {
        
    }
}
