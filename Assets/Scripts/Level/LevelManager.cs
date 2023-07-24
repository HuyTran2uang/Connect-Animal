using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviourSingleton<LevelManager>, IReadData, IPrepareGame
{
    private int _level;
    List<ILevelText> _levelTexts = new List<ILevelText>();
    List<ILevelMapText> _levelMapTexts = new List<ILevelMapText>();

    public int Level => _level;

    public void LevelUp()
    {
        _level++;
        Data.WriteData.Save(GlobalKey.LEVEL, _level);
        if(_level == 12)
        {
            SpecialSpawner.Instance.AddSpecialType(SpecialType.Bomb);
            SpecialSpawner.Instance.AddSpecialType(SpecialType.Lightning);
        }
        _levelTexts.ForEach(i => i.SetLevelText(Level));
    }

    public void SetLevelMap()
    {
        _levelMapTexts.ForEach(i => i.SetLevelText(_level));
    }

    public void LoadData()
    {
        _level = Data.ReadData.LoadData(GlobalKey.LEVEL, 1);
    }

    public void Prepare()
    {
        _levelTexts = FindObjectsOfType<MonoBehaviour>(true).OfType<ILevelText>().ToList();
        _levelMapTexts = FindObjectsOfType<MonoBehaviour>(true).OfType<ILevelMapText>().ToList();
        _levelTexts.ForEach(i => i.SetLevelText(Level));
        if(_level > 12)
        {
            SpecialSpawner.Instance.AddSpecialType(SpecialType.Bomb);
            SpecialSpawner.Instance.AddSpecialType(SpecialType.Lightning);
        }
    }
}
