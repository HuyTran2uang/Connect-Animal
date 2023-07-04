using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConfig
{
    private int _row, _col;
    private List<Vector2> _nullPoses;

    public LevelConfig(int row, int col, List<Vector2> nullPoses)
    {
        _row = row;
        _col = col;
        _nullPoses = nullPoses;
    }

    public int Row => _row;
    public int Col => _col;
    public List<Vector2> NullPoses => _nullPoses;
}
