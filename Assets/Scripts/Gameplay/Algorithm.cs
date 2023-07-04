using System;
using UnityEngine;

public class Algorithm
{
    private int _row, _col, _notBarrier = 0;
    private int[][] _matrix;

    public Algorithm(int row, int col)
    {
        this._row = row;
        this._col = col;
        Debug.Log(row + "," + col);
        //create _matrix
        ShowMatrix();
    }

    public void ShowMatrix()
    {
        for (int i = 1; i < _row - 1; i++)
        {
            for (int j = 1; j < _col - 1; j++)
            {
                Debug.Log(_matrix[i][j]);
            }
            Debug.Log("\n");
        }
    }

    private bool CheckLineX(int y1, int y2, int x)
    {
        Debug.Log("check line x");
        // find point have column max and min
        int min = Mathf.Min(y1, y2);
        int max = Mathf.Max(y1, y2);
        // run column
        for (int y = min + 1; y < max; y++)
        {
            if (_matrix[x][y] > _notBarrier)
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
        Debug.Log("check line y");
        int min = Mathf.Min(x1, x2);
        int max = Mathf.Max(x1, x2);
        for (int x = min + 1; x < max; x++)
        {
            if (_matrix[x][y] > _notBarrier)
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
            if (y > pMinY.y && _matrix[pMinY.x][y] > _notBarrier)
            {
                return -1;
            }
            // check two line
            if ((_matrix[pMaxY.x][y] == _notBarrier || y == pMaxY.y)
                    && CheckLineY(pMinY.x, pMaxY.x, y)
                    && CheckLineX(y, pMaxY.y, pMaxY.x))
            {

                Debug.Log("Rect x");
                Debug.Log("(" + pMinY.x + "," + pMinY.y + ") -> ("
                        + pMinY.x + "," + y + ") -> (" + pMaxY.x + "," + y
                        + ") -> (" + pMaxY.x + "," + pMaxY.y + ")");
                // if three line is true return column y
                return y;
            }
        }
        // have a line in three line not true then return -1
        return -1;
    }

    private int CheckRectY(Point p1, Point p2)
    {
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
            if (x > pMinX.x && _matrix[x][pMinX.y] > _notBarrier)
            {
                return -1;
            }
            if ((_matrix[x][pMaxX.y] == _notBarrier || x == pMaxX.x)
                    && CheckLineX(pMinX.y, pMaxX.y, x)
                    && CheckLineY(x, pMaxX.x, pMaxX.y))
            {

                Debug.Log("Rect y");
                Debug.Log("(" + pMinX.x + "," + pMinX.y + ") -> (" + x
                        + "," + pMinX.y + ") -> (" + x + "," + pMaxX.y
                        + ") -> (" + pMaxX.x + "," + pMaxX.y + ")");
                return x;
            }
        }
        return -1;
    }

    /**
	 * PointA and PointB are Points want check
	 * 
	 * @param type
	 *            : true is check with increase, false is decrease return column
	 *            can connect PointA and PointB
	 */
    private int CheckMoreLineX(Point p1, Point p2, int type)
    {
        Debug.Log("check chec more x");
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
        if ((_matrix[row][colFinish] == _notBarrier || pMinY.y == pMaxY.y)
                && CheckLineX(pMinY.y, pMaxY.y, row))
        {
            while (_matrix[pMinY.x][y] == _notBarrier
                    && _matrix[pMaxY.x][y] == _notBarrier)
            {
                if (CheckLineY(pMinY.x, pMaxY.x, y))
                {

                    Debug.Log("TH X " + type);
                    Debug.Log("(" + pMinY.x + "," + pMinY.y + ") -> ("
                            + pMinY.x + "," + y + ") -> (" + pMaxY.x + "," + y
                            + ") -> (" + pMaxY.x + "," + pMaxY.y + ")");
                    return y;
                }
                y += type;
            }
        }
        return -1;
    }

    private int checkMoreLineY(Point p1, Point p2, int type)
    {
        Debug.Log("check more y");
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
        if ((_matrix[rowFinish][col] == _notBarrier || pMinX.x == pMaxX.x)
                && CheckLineY(pMinX.x, pMaxX.x, col))
        {
            while (_matrix[x][pMinX.y] == _notBarrier
                    && _matrix[x][pMaxX.y] == _notBarrier)
            {
                if (CheckLineX(pMinX.y, pMaxX.y, x))
                {
                    Debug.Log("TH Y " + type);
                    Debug.Log("(" + pMinX.x + "," + pMinX.y + ") -> ("
                            + x + "," + pMinX.y + ") -> (" + x + "," + pMaxX.y
                            + ") -> (" + pMaxX.x + "," + pMaxX.y + ")");
                    return x;
                }
                x += type;
            }
        }
        return -1;
    }

    public Line CheckTwoPoint(Point p1, Point p2)
    {
        if (!p1.Equals(p2) && _matrix[p1.x][p1.y] == _matrix[p2.x][p2.y])
        {
            // check line with x
            if (p1.x == p2.x)
            {
                Debug.Log("line x");
                if (CheckLineX(p1.y, p2.y, p1.x))
                {
                    return new Line(p1, p2);
                }
            }
            // check line with y
            if (p1.y == p2.y)
            {
                Debug.Log("line y");
                if (CheckLineY(p1.x, p2.x, p1.y))
                {
                    Debug.Log("ok line y");
                    return new Line(p1, p2);
                }
            }

            int t = -1; // t is column find

            // check in rectangle with x
            if ((t = CheckRectX(p1, p2)) != -1)
            {
                Debug.Log("rect x");
                return new Line(new Point(p1.x, t), new Point(p2.x, t));
            }

            // check in rectangle with y
            if ((t = CheckRectY(p1, p2)) != -1)
            {
                Debug.Log("rect y");
                return new Line(new Point(t, p1.y), new Point(t, p2.y));
            }
            // check more right
            if ((t = CheckMoreLineX(p1, p2, 1)) != -1)
            {
                Debug.Log("more right");
                return new Line(new Point(p1.x, t), new Point(p2.x, t));
            }
            // check more left
            if ((t = CheckMoreLineX(p1, p2, -1)) != -1)
            {
                Debug.Log("more left");
                return new Line(new Point(p1.x, t), new Point(p2.x, t));
            }
            // check more down
            if ((t = checkMoreLineY(p1, p2, 1)) != -1)
            {
                Debug.Log("more down");
                return new Line(new Point(t, p1.y), new Point(t, p2.y));
            }
            // check more up
            if ((t = checkMoreLineY(p1, p2, -1)) != -1)
            {
                Debug.Log("more up");
                return new Line(new Point(t, p1.y), new Point(t, p2.y));
            }
        }
        return null;
    }

    public int GetRow()
    {
        return _row;
    }

    public void SetRow(int row)
    {
        this._row = row;
    }

    public int GetCol()
    {
        return _col;
    }

    public void SetCol(int col)
    {
        this._col = col;
    }

    public int[][] GetMatrix()
    {
        return _matrix;
    }

    public void SetMatrix(int[][] matrix)
    {
        this._matrix = matrix;
    }
}
