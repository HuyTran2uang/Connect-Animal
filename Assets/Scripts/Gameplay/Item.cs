using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _iconSR;
    private int _row, _col;

    public void Init(int row, int column, Sprite icon)
    {
        _row = row;
        _col = column;
        _iconSR.sprite = icon;
    }

    public void Select()
    {
        BoardManager.Instance.SelectNode(_row, _col);
    }
}
