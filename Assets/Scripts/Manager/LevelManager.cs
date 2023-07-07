using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviourSingleton<LevelManager>, IReadData, IPrepareGame
{
    private const int LEVEL_BEGIN = 1;
    private int _level;

    public int Level => _level;

    public void LevelUp()
    {
        _level++;
        Data.WriteData.Save(GlobalKey.LEVEL, _level);
    }

    public void LoadData()
    {
        _level = Data.ReadData.LoadData(GlobalKey.LEVEL, LEVEL_BEGIN);
    }

    public void Prepare()
    {
        //set ui
    }
}
