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
        Debug.Log("CONVERTER");
        //var stringMatrix = File.ReadAllText($"./Assets/Resources/Levels/Level_{level}.txt");
        var stringMatrix = Resources.Load<TextAsset>($"Levels/Level_{level}").text;
        int totalVals = stringMatrix.ToList().Count(i => i == '1');
        int totalRows = stringMatrix.ToList().Count(i => i == '{');
        //
        var pattern = @"\{(.+?)\}";

        var matches = Regex.Matches(stringMatrix, pattern).Select(i => i.ToString().ReplaceMultiple(new HashSet<char>() { '{', '}' }, ' ')).ToList();
        Debug.Log("CONVERTER 1");
        List<List<int>> listFormatGrid = new List<List<int>>();
        foreach (var match in matches)
            listFormatGrid.Add(match.Split(',').Select(i => int.Parse(i)).ToList());
        Debug.Log("CONVERTER 2");
        var totalCols = matches[0].Split(',').Count();
        int[,] grid = new int[totalRows, totalCols];
        for (int i = 0; i < totalRows; i++)
        {
            for (int j = 0; j < totalCols; j++)
            {
                grid[i,j] = listFormatGrid[i][j];
            }
        }
        Debug.Log("COMPLETED CONVERT");
        return new LevelConfig(totalRows ,totalCols, grid, totalVals);
    }
}
