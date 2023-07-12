using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConfig
{
    private int _totalRows, _totalCols, _totalVals;
    private int[,] _grid;

    public LevelConfig(int row, int col, int[,] grid, int totalVals)
    {
        _totalRows = row;
        _totalCols = col;
        _grid = grid;
        _totalVals = totalVals;
    }

    public int TotalRows => _totalRows;
    public int TotalCols => _totalCols;
    public int TotalVals => _totalVals;
    public int[,] Grid => _grid;
}
