using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviourSingleton<LevelManager>
{
    private int _level = 2;

    public int Level => _level;

    public void LevelUp()
    {
        _level++;
    }
}
