using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeNodeFall : MonoBehaviourSingleton<MergeNodeFall>
{
    [SerializeField] GameObject _redLinePrefab, _redCirclePrefab;
    List<GameObject> _lineAndCircles = new List<GameObject>();

    private string PathFall(int x1, int y1, int x2, int y2, int[,] matrix)
    {
        string path = "";
        if (x1 <= x2)
        {
            for (int i = x1; i < x2; i++)
            {
                path += "D";
            }
            if(y1 <= y2)
            {
                for (int i = y1; i < y2; i++)
                {
                    path += "R";
                }
            }
            else
            {
                for (int i = y2; i < y1; i++)
                {
                    path += "L";
                }
            }
        }
        else
        {
            for (int i = x2; i < x1; i++)
            {
                path += "U";
            }
            if (y1 <= y2)
            {
                for (int i = y1; i < y2; i++)
                {
                    path += "R";
                }
            }
            else
            {
                for (int i = y2; i < y1; i++)
                {
                    path += "L";
                }
            }
        }
        return path;
    }

    public void ShowPathFall(int x1, int y1, int x2, int y2, int[,] matrix)
    {
        string path = PathFall(x1, y1, x2, y2, matrix);
        Vector3 pos = BoardManager.Instance.GetPosFrom(x1, y1);
        for (int i = 0; i < path.Length; i++)
        {
            GameObject redLine = Instantiate(_redLinePrefab, pos, Quaternion.identity, transform);
            _lineAndCircles.Add(redLine);
            if(i == 0)
            {
                GameObject redCircle = Instantiate(_redCirclePrefab, pos, Quaternion.identity, transform);
                _lineAndCircles.Add(redCircle);
            }
            switch (path[i])
            {
                case 'L':
                    pos += Vector3.left;
                    redLine.transform.localEulerAngles += new Vector3(0, 0, 180);
                    break;
                case 'R':
                    pos += Vector3.right;
                    redLine.transform.localEulerAngles += new Vector3(0, 0, 0);
                    break;
                case 'U':
                    pos += Vector3.up;
                    redLine.transform.localEulerAngles += new Vector3(0, 0, 90);
                    break;
                case 'D':
                    pos += Vector3.down;
                    redLine.transform.localEulerAngles += new Vector3(0, 0, -90);
                    break;
            }
            if (i < path.Length - 1 && path[i] != path[i + 1] || i == path.Length - 1)
            {
                GameObject redCircle = Instantiate(_redCirclePrefab, pos, Quaternion.identity, transform);
                _lineAndCircles.Add(redCircle);
            }
        }

        DOVirtual.DelayedCall(1f, ClearLines);
    }

    private void ClearLines()
    {
        for (int i = _lineAndCircles.Count - 1; i >= 0; i--)
            Destroy(_lineAndCircles[i]);
        _lineAndCircles.Clear();
    }
}
