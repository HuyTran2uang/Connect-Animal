using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private int _x, _y, _val;

    public Node(int x, int y, int val)
    {
        _x = x;
        _y = y;
        _val = val;
    }

    public int X => _x;
    public int Y => _y;
    public int Val => _val;
}
