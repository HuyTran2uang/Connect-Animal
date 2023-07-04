using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConfigStorage
{
    public static List<LevelConfig> LevelConfigs = new List<LevelConfig>()
    {
        null, // 0
        new LevelConfig(3, 4, null), // 1
        new LevelConfig(4, 5, null), // 2
        new LevelConfig(6, 5, null), // 3
        new LevelConfig(8, 6, new List<Vector2>()
        {
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(0, 2),
            new Vector2(1, 0),
            new Vector2(2, 0),
            new Vector2(1, 1),
        }), // 4
    };
}
