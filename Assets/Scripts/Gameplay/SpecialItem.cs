using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialItem : Item
{
    public void Init(int row, int col, int id)
    {
        _row = row;
        _col = col;
        _id = id;
    }
}
