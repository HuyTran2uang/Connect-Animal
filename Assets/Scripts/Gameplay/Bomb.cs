using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bomb : MonoBehaviour
{
    int _row, _col;

    public void Target(int row, int col)
    {
        _row = row;
        _col = col;
        Vector3 targetPos = BoardManager.Instance.GetPosFrom(row, col);
        MoveToTarget(targetPos);
    }

    private void MoveToTarget(Vector3 targetPos)
    {
        Vector3 direction = (targetPos - (Vector3)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var offset = -90f;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
        transform.DOMove(targetPos, .5f).OnComplete(MoveCompleted);
    }

    protected virtual void MoveCompleted()
    {
        BombManager.Instance.OnBombExplode(this);
    }

    public void Explode()
    {
        BoardManager.Instance.ExplodeAndRemoveItem(_row, _col);
    }
}
