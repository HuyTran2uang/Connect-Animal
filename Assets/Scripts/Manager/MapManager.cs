using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    int[,] _grid;
    int _totalIds, _totalRows, _totalCols;

    public int[,] GetGrid() => _grid;
    public int GetTotalIds() => _totalIds;
    public int GetTotalRows() => _totalRows;
    public int GetTotalCols() => _totalCols;

    //4x6/NoTopLeft(2);NoBottomRight(2)/U
    public void CreateMap(string code)
    {

    }

    public void SetGrid(int totalRows, int totalCols)
    {
        _totalRows = totalRows;
        _totalCols = totalCols;
        _grid = new int[_totalRows, _totalCols];
        for (int row = 0; row < _totalRows; row++)
            for (int col = 0; col < _totalCols; col++)
                _grid[row, col] = 1;
    }
    
    //shape
    private void NoTopLeft(int radius)
    {
        for (int row = 0; row < _totalRows; row++)
            for (int col = 0; col < _totalCols; col++)
                if (row < radius || col < radius)
                    _grid[row, col] = 0;
    }

    private void NoBottomRight(int radius)
    {
        for (int row = _totalRows - 1; row >= 0; row--)
            for (int col = _totalCols; col >= 0; col--)
                if (row > _totalRows - radius || col > _totalCols - radius)
                    _grid[row, col] = 0;
    }
    //movement
    private void CanMoveUp()
    {

    }

    private void CanMoveDown()
    {

    }

    private void CanMoveLeft()
    {

    }

    private void CanMoveRight()
    {

    }
}
