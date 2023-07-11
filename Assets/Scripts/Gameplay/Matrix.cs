using System.Collections.Generic;
using UnityEngine;

public class Matrix
{
    private int _notBarrier = -1;
    private int[,] _matrix;
    List<Vector2> _points = new List<Vector2>();

    public Matrix(int[,] matrix, int totalRows, int totalCols)
    {
        _matrix = matrix;
        //ShowMaxtrix(totalRows, totalCols);
    }

    private void ShowMaxtrix(int totalRows, int totalCols)
    {
        string matrixStr = "";
        for (int row = 0; row < totalRows; row++)
        {
            for(int col = 0; col < totalCols; col++)
            {
                matrixStr += $"{_matrix[row, col]} ";
            }
            matrixStr += "\n";
        }
        Debug.Log(matrixStr);
    }

    // check with line x, from column y1 to y2
    private bool CheckLineX(int y1, int y2, int x)
    {
        int min = Mathf.Min(y1, y2);
        int max = Mathf.Max(y1, y2);
        // run column
        for (int y = min + 1; y < max; y++)
            if (_matrix[x,y] > _notBarrier)
                return false;
        return true;
    }

    private bool CheckLineY(int x1, int x2, int y)
    {
        int min = Mathf.Min(x1, x2);
        int max = Mathf.Max(x1, x2);
        for (int x = min + 1; x < max; x++)
            if (_matrix[x,y] > _notBarrier)
                return false;
        return true;
    }

    // check in rectangle
    private int CheckRectX(Point p1, Point p2)
    {
        Point pMinY = p1, pMaxY = p2;
        if (p1.y > p2.y)
        {
            pMinY = p2;
            pMaxY = p1;
        }
        for (int y = pMinY.y; y <= pMaxY.y; y++)
        {
            if (y > pMinY.y && _matrix[pMinY.x,y] > _notBarrier)
                return -1;
            if ((_matrix[pMaxY.x,y] == _notBarrier || y == pMaxY.y)
                    && CheckLineY(pMinY.x, pMaxY.x, y)
                    && CheckLineX(y, pMaxY.y, pMaxY.x))
            {
                _points = new List<Vector2>()
                {
                    new Vector2(pMinY.x, pMinY.y),
                    new Vector2(pMinY.x, y),
                    new Vector2(pMaxY.x, y),
                    new Vector2(pMaxY.x, pMaxY.y),
                };
                return y;
            }
        }
        return -1;
    }

    private int CheckRectY(Point p1, Point p2)
    {
        Point pMinX = p1, pMaxX = p2;
        if (p1.x > p2.x)
        {
            pMinX = p2;
            pMaxX = p1;
        }
        for (int x = pMinX.x; x <= pMaxX.x; x++)
        {
            if (x > pMinX.x && _matrix[x,pMinX.y] > _notBarrier)
                return -1;
            if ((_matrix[x, pMaxX.y] == _notBarrier || x == pMaxX.x)
                    && CheckLineX(pMinX.y, pMaxX.y, x)
                    && CheckLineY(x, pMaxX.x, pMaxX.y))
            {
                _points = new List<Vector2>()
                {
                    new Vector2(pMinX.x, pMinX.y),
                    new Vector2(x, pMinX.y),
                    new Vector2(x, pMaxX.y),
                    new Vector2(pMaxX.x, pMaxX.y),
                };
                return x;
            }
        }
        return -1;
    }

    private int CheckMoreLineX(Point p1, Point p2, int type)
    {
        Point pMinY = p1, pMaxY = p2;
        if (p1.y > p2.y)
        {
            pMinY = p2;
            pMaxY = p1;
        }
        int y = pMaxY.y + type;
        int row = pMinY.x;
        int colFinish = pMaxY.y;
        if (type == -1)
        {
            colFinish = pMinY.y;
            y = pMinY.y + type;
            row = pMaxY.x;
        }
        if ((_matrix[row, colFinish] == _notBarrier || pMinY.y == pMaxY.y)
                && CheckLineX(pMinY.y, pMaxY.y, row))
        {
            while (_matrix[pMinY.x, y] == _notBarrier
                    && _matrix[pMaxY.x, y] == _notBarrier)
            {
                if (CheckLineY(pMinY.x, pMaxY.x, y))
                {
                    _points = new List<Vector2>()
                    {
                        new Vector2(pMinY.x, pMinY.y),
                        new Vector2(pMinY.x, y),
                        new Vector2(pMaxY.x, y),
                        new Vector2(pMaxY.x, pMaxY.y),
                    };
                    return y;
                }
                y += type;
            }
        }
        return -1;
    }

    private int CheckMoreLineY(Point p1, Point p2, int type)
    {
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
                    _points = new List<Vector2>()
                    {
                        new Vector2(pMinX.x, pMinX.y),
                        new Vector2(x, pMinX.y),
                        new Vector2(x, pMaxX.y),
                        new Vector2(pMaxX.x, pMaxX.y),
                    };
                    return x;
                }
                x += type;
            }
        }
        return -1;
    }

    public List<Vector2> GetPath(Point p1, Point p2)
    {
        _points = new List<Vector2>();
        if (!p1.Equals(p2) && _matrix[p1.x, p1.y] == _matrix[p2.x, p2.y])
        {
            if (p1.x == p2.x)
            {
                if (CheckLineX(p1.y, p2.y, p1.x))
                {
                    _points = new List<Vector2>()
                    {
                        new Vector2(p1.x, p1.y),
                        new Vector2(p2.x, p2.y),
                    };
                    return _points;
                }
            }
            if (p1.y == p2.y)
            {
                if (CheckLineY(p1.x, p2.x, p1.y))
                {
                    _points = new List<Vector2>()
                    {
                        new Vector2(p1.x, p1.y),
                        new Vector2(p2.x, p2.y),
                    };
                    return _points;
                }
            }

            if ((CheckRectX(p1, p2)) != -1)
                return _points;
            if ((CheckRectY(p1, p2)) != -1)
                return _points;
            if ((CheckMoreLineX(p1, p2, 1)) != -1)
                return _points;
            if ((CheckMoreLineX(p1, p2, -1)) != -1)
                return _points;
            if ((CheckMoreLineY(p1, p2, 1)) != -1)
                return _points;
            if ((CheckMoreLineY(p1, p2, -1)) != -1)
                return _points;
        }
        return null;
    }
}
