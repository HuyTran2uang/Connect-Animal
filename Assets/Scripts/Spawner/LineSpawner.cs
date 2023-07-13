using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSpawner : MonoBehaviourSingleton<LineSpawner>
{
    [SerializeField] GameObject _linePrefab;
    private List<GameObject> _lines = new List<GameObject>();

    private GameObject LineSpawned(Vector3 pos, char dir)
    {
        GameObject line = Instantiate(_linePrefab, pos, Quaternion.identity, transform);
        switch (dir)
        {
            case 'R':
                break;
            case 'L':
                line.transform.localEulerAngles = new Vector3(0, 0, 180);
                break;
            case 'D':
                line.transform.localEulerAngles = new Vector3(0, 0, -90);
                break;
            case 'U':
                line.transform.localEulerAngles = new Vector3(0, 0, 90);
                break;
        }
        _lines.Add(line);
        return line;
    }

    public void ClearLines()
    {
        for (int i = _lines.Count - 1; i >= 0; i--)
            Destroy(_lines[i]);
        _lines.Clear();
    }

    private string StepA2B(Vector2Int p1, Vector2Int p2)
    {
        string path = "";
        if (p1.y == p2.y)
        {
            if (p1.x < p2.x)
                for (int i = 0; i < p2.x - p1.x; i++)
                {
                    path += "U";
                }
            if (p1.x > p2.x)
                for (int i = 0; i < p1.x - p2.x; i++)
                {
                    path += "D";
                }
        }
        if (p1.x == p2.x)
        {
            if (p1.y < p2.y)
                for (int i = 0; i < p2.y - p1.y; i++)
                {
                    path += "L";
                }
            if (p1.y > p2.y)
                for (int i = 0; i < p1.y - p2.y; i++)
                {
                    path += "R";
                }
        }
        return path;
    }

    private string ConvertToStringPath(List<Vector2Int> points)
    {
        string path = "";
        for (int i = 0; i < points.Count - 1; i++)
            path += StepA2B(points[i + 1], points[i]);
        return path;
    }

    public void Concatenate(List<Vector2Int> points)
    {
        Vector3 curPos = BoardManager.Instance.GetPosFrom(points[0].x, points[0].y);
        string path = ConvertToStringPath(points);
        foreach (char dir in path)
        {
            LineSpawned(curPos, dir);
            switch (dir)
            {
                case 'R':
                    StarSpawner.Instance.StarSpawned(curPos);
                    curPos += Vector3.right;
                    break;
                case 'L':
                    StarSpawner.Instance.StarSpawned(curPos);
                    curPos += Vector3.left;
                    break;
                case 'D':
                    StarSpawner.Instance.StarSpawned(curPos);
                    curPos += Vector3.down;
                    break;
                case 'U':
                    StarSpawner.Instance.StarSpawned(curPos);
                    curPos += Vector3.up;
                    break;
            }
            StarSpawner.Instance.StarSpawned(curPos);
        }
    }
}
