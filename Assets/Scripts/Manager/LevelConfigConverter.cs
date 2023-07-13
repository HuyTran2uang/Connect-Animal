using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

public class LevelConfigConverter
{
    public static LevelConfig GetLevelConfig(int level)
    {
        var stringMatrix = File.ReadAllText($"Assets/Resources/Levels/Level_{level}.txt");
        int totalVals = stringMatrix.ToList().Count(i => i == '1');
        int totalRows = stringMatrix.ToList().Count(i => i == '{');
        //
        var pattern = @"\{(.+?)\}";

        var matches = Regex.Matches(stringMatrix, pattern).Select(i => i.ToString().ReplaceMultiple(new HashSet<char>() { '{', '}' }, ' ')).ToList();
        List<List<int>> listFormatGrid = new List<List<int>>();
        foreach (var match in matches)
            listFormatGrid.Add(match.Split(',').Select(i => int.Parse(i)).ToList());
        var totalCols = matches[0].Split(',').Count();
        int[,] grid = new int[totalRows, totalCols];
        for (int i = 0; i < totalRows; i++)
        {
            for (int j = 0; j < totalCols; j++)
            {
                grid[i,j] = listFormatGrid[i][j];
            }
        }
        return new LevelConfig(totalRows ,totalCols, grid, totalVals);
    }
}
