using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private int _x, _y, _val;
    private Vector3 _pos;
    private Node _nodeSwap;

    public Node(int x, int y, int val, Vector3 pos)
    {
        _x = x;
        _y = y;
        _val = val;
        _pos = pos;
    }

    public int X => _x;
    public int Y => _y;
    public int Val => _val;
    public Vector3 Pos => _pos;
    public Node NodeSwap => _nodeSwap;

    public void ChangeVal(int newVal)
    {
        _val = newVal;
    }

    public void SetNodeSwap(Node node)
    {
        _nodeSwap = node;
    }

    public void ChangePos(Vector3 newPos)
    {
        _pos = newPos;
    }
}
