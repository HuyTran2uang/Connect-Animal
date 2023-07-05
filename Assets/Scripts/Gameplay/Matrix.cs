using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

[Serializable]
public class Matrix
{
    private int _notBarrier = -1;
    private int[,] _matrix;
    [SerializeField] string _path;
    [SerializeField] List<Vector2> _points = new List<Vector2>();

    public Matrix(int[,] matrix)
    {
        _matrix = matrix;
    }

    // check with line x, from column y1 to y2
    private bool CheckLineX(int y1, int y2, int x)
    {
        int min = Mathf.Min(y1, y2);
        int max = Mathf.Max(y1, y2);
        // run column
        for (int y = min + 1; y < max; y++)
        {
            if (_matrix[x,y] > _notBarrier)
            { // if see barrier then die
                Debug.Log("die: " + x + "" + y);
                return false;
            }
            Debug.Log("ok: " + x + "" + y);
        }
        // not die -> success
        return true;
    }

    private bool CheckLineY(int x1, int x2, int y)
    {
        int min = Mathf.Min(x1, x2);
        int max = Mathf.Max(x1, x2);
        for (int x = min + 1; x < max; x++)
        {
            if (_matrix[x,y] > _notBarrier)
            {
                Debug.Log("die: " + x + "" + y);
                return false;
            }
            Debug.Log("ok: " + x + "" + y);
        }
        return true;
    }

    // check in rectangle
    private int CheckRectX(Point p1, Point p2)
    {
        _path = "";
        Debug.Log("check rect x");
        // find point have y min and max
        Point pMinY = p1, pMaxY = p2;
        if (p1.y > p2.y)
        {
            pMinY = p2;
            pMaxY = p1;
        }
        for (int y = pMinY.y; y <= pMaxY.y; y++)
        {
            if (y > pMinY.y && _matrix[pMinY.x,y] > _notBarrier)
            {
                Debug.Log("Touch barrier");
                return -1;
            }
            // check two line
            if ((_matrix[pMaxY.x,y] == _notBarrier || y == pMaxY.y)
                    && CheckLineY(pMinY.x, pMaxY.x, y)
                    && CheckLineX(y, pMaxY.y, pMaxY.x))
            {
                Debug.Log("Rect x");
                Debug.Log("(" + pMinY.x + "," + pMinY.y + ") -> ("
                        + pMinY.x + "," + y + ") -> (" + pMaxY.x + "," + y
                        + ") -> (" + pMaxY.x + "," + pMaxY.y + ")");
                _points = new List<Vector2>()
                {
                    new Vector2(pMinY.x, pMinY.y),
                    new Vector2(pMinY.x, y),
                    new Vector2(pMaxY.x, y),
                    new Vector2(pMaxY.x, pMaxY.y),
                };
                ConvertToStringPath();
                // if three line is true return column y
                return y;
            }
        }
        // have a line in three line not true then return -1
        return -1;
    }

    private int CheckRectY(Point p1, Point p2)
    {
        _path = "";
        Debug.Log("check rect y");
        // find point have y min
        Point pMinX = p1, pMaxX = p2;
        if (p1.x > p2.x)
        {
            pMinX = p2;
            pMaxX = p1;
        }
        // find line and y begin
        for (int x = pMinX.x; x <= pMaxX.x; x++)
        {
            if (x > pMinX.x && _matrix[x,pMinX.y] > _notBarrier)
            {
                Debug.Log("Touch barrier");
                return -1;
            }
            if ((_matrix[x, pMaxX.y] == _notBarrier || x == pMaxX.x)
                    && CheckLineX(pMinX.y, pMaxX.y, x)
                    && CheckLineY(x, pMaxX.x, pMaxX.y))
            {
                Debug.Log("Rect y");
                Debug.Log("(" + pMinX.x + "," + pMinX.y + ") -> (" + x
                        + "," + pMinX.y + ") -> (" + x + "," + pMaxX.y
                        + ") -> (" + pMaxX.x + "," + pMaxX.y + ")");
                _points = new List<Vector2>()
                {
                    new Vector2(pMinX.x, pMinX.y),
                    new Vector2(x, pMinX.y),
                    new Vector2(x, pMinX.y),
                    new Vector2(pMaxX.x, pMaxX.y),
                };
                ConvertToStringPath();
                return x;
            }
        }
        return -1;
    }

    private int CheckMoreLineX(Point p1, Point p2, int type)
    {
        _path = "";
        _points = new List<Vector2>();
        Debug.Log($"check chec more x {type}");
        // find point have y min
        Point pMinY = p1, pMaxY = p2;
        if (p1.y > p2.y)
        {
            pMinY = p2;
            pMaxY = p1;
        }
        // find line and y begin
        int y = pMaxY.y + type;
        int row = pMinY.x;
        int colFinish = pMaxY.y;
        if (type == -1)
        {
            colFinish = pMinY.y;
            y = pMinY.y + type;
            row = pMaxY.x;
            Debug.Log("colFinish = " + colFinish);
        }

        // find column finish of line

        // check more
        if ((_matrix[row, colFinish] == _notBarrier || pMinY.y == pMaxY.y)
                && CheckLineX(pMinY.y, pMaxY.y, row))
        {
            while (_matrix[pMinY.x, y] == _notBarrier
                    && _matrix[pMaxY.x, y] == _notBarrier)
            {
                if (CheckLineY(pMinY.x, pMaxY.x, y))
                {
                    Debug.Log("TH X " + type);
                    Debug.Log("(" + pMinY.x + "," + pMinY.y + ") -> ("
                            + pMinY.x + "," + y + ") -> (" + pMaxY.x + "," + y
                            + ") -> (" + pMaxY.x + "," + pMaxY.y + ")");
                    _points = new List<Vector2>()
                    {
                        new Vector2(pMinY.x, pMinY.y),
                        new Vector2(pMinY.x, y),
                        new Vector2(pMaxY.x, y),
                        new Vector2(pMaxY.x, pMaxY.y),
                    };
                    ConvertToStringPath();
                    return y;
                }
                y += type;
            }
        }
        return -1;
    }

    private int CheckMoreLineY(Point p1, Point p2, int type)
    {
        _path = "";
        _points = new List<Vector2>();
        Debug.Log($"check more y {type}");
        Point pMinX = p1, pMaxX = p2;
        if (p1.x > p2.x)
        {
            pMinX = p2;
            pMaxX = p1;
        }
        int x = pMaxX.x + type;
        int col = pMinX.y;
        int rowFinish = pMaxX.x;
        if (type == -1)
        {
            rowFinish = pMinX.x;
            x = pMinX.x + type;
            col = pMaxX.y;
        }
        if ((_matrix[rowFinish, col] == _notBarrier || pMinX.x == pMaxX.x)
                && CheckLineY(pMinX.x, pMaxX.x, col))
        {
            while (_matrix[x, pMinX.y] == _notBarrier
                    && _matrix[x, pMaxX.y] == _notBarrier)
            {
                if (CheckLineX(pMinX.y, pMaxX.y, x))
                {
                    Debug.Log("TH Y " + type);
                    Debug.Log("(" + pMinX.x + "," + pMinX.y + ") -> ("
                            + x + "," + pMinX.y + ") -> (" + x + "," + pMaxX.y
                            + ") -> (" + pMaxX.x + "," + pMaxX.y + ")");
                    _points = new List<Vector2>()
                    {
                        new Vector2(pMinX.x, pMinX.y),
                        new Vector2(x, pMinX.y),
                        new Vector2(x, pMaxX.y),
                        new Vector2(pMaxX.x, pMaxX.y),
                    };
                    ConvertToStringPath();
                    return x;
                }
                x += type;
            }
        }
        return -1;
    }

    private void StepA2B(Vector2 p1, Vector2 p2)
    {
        if (p1.y == p2.y)
        {
            if (p1.x < p2.x)
                for (int i = 0; i < p2.x - p1.x; i++)
                {
                    _path += "U";
                }
            if (p1.x > p2.x)
                for (int i = 0; i < p1.x - p2.x; i++)
                {
                    _path += "D";
                }
        }
        if (p1.x == p2.x)
        {
            if (p1.y < p2.y)
                for (int i = 0; i < p2.y - p1.y; i++)
                {
                    _path += "L";
                }
            if (p1.y > p2.y)
                for (int i = 0; i < p1.y - p2.y; i++)
                {
                    _path += "R";
                }
        }
    }

    private void ConvertToStringPath()
    {
        for (int i = 0; i < _points.Count - 1; i++)
        {
            StepA2B(_points[i + 1], _points[i]);
        }
    }

    public string GetPath(Point p1, Point p2)
    {
        _path = "";
        if (!p1.Equals(p2) && _matrix[p1.x, p1.y] == _matrix[p2.x, p2.y])
        {
            // check line with x
            if (p1.x == p2.x)
            {
                Debug.Log("line x");
                if (CheckLineX(p1.y, p2.y, p1.x))
                {
                    _points = new List<Vector2>()
                    {
                        new Vector2(p1.x, p1.y),
                        new Vector2(p2.x, p2.y),
                    };
                    ConvertToStringPath();
                    return _path;
                }
            }
            // check line with y
            if (p1.y == p2.y)
            {
                Debug.Log("line y");
                if (CheckLineY(p1.x, p2.x, p1.y))
                {
                    _points = new List<Vector2>()
                    {
                        new Vector2(p1.x, p1.y),
                        new Vector2(p2.x, p2.y),
                    };
                    ConvertToStringPath();
                    return _path;
                }
            }

            int t = -1; // t is column find

            // check in rectangle with x
            if ((t = CheckRectX(p1, p2)) != -1)
            {
                Debug.Log("rect x");
                return _path;
            }

            // check in rectangle with y
            if ((t = CheckRectY(p1, p2)) != -1)
            {
                Debug.Log("rect y");
                return _path;
            }
            // check more right
            if ((t = CheckMoreLineX(p1, p2, 1)) != -1)
            {
                Debug.Log("more right");
                return _path;
            }
            // check more left
            if ((t = CheckMoreLineX(p1, p2, -1)) != -1)
            {
                Debug.Log("more left");
                return _path;
            }
            // check more down
            if ((t = CheckMoreLineY(p1, p2, 1)) != -1)
            {
                Debug.Log("more down");
                return _path;
            }
            // check more up
            if ((t = CheckMoreLineY(p1, p2, -1)) != -1)
            {
                Debug.Log("more up");
                return _path;
            }
        }
        _path = "";
        return null;
    }

    public void CheckSuccess(Point p1, Point p2)
    {
        _matrix[p1.x, p1.y] = -1;
        _matrix[p2.x, p2.y] = -1;
    }
}
