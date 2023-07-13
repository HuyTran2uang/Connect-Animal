using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortGrid
{
    public static int[,] Up(int[,] grid, int totalRows, int totalCols)
    {
        for (int col = 0; col < totalCols; col++)
        {
            for (int row = totalRows - 1; row >= 0; row--)
            {
                int f_row = row;
                while (true)
                {
                    if (grid[f_row - 1, col] != 0 || f_row == 0) break;
                    grid[f_row - 1, col] = grid[f_row, col];
                    grid[f_row, col] = 0;
                    row--;
                }
            }
        }
        return grid;
    }

    public static int[,] Down()
    {
        return null;
    }

    public static int[,] Left()
    {
        return null;
    }

    public static int[,] Right()
    {
        return null;
    }
}
