using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviourSingleton<LevelManager>, IReadData, IPrepareGame
{
    private int _level;
    List<IChangeLevelText> _changeLevelTexts = new List<IChangeLevelText>();

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
        _changeLevelTexts.ForEach(i => i.ChangeLevelText(Level));
    }

    public void LoadData()
    {
        _level = Data.ReadData.LoadData(GlobalKey.LEVEL, 1);
    }

    public void Prepare()
    {
        _changeLevelTexts = FindObjectsOfType<MonoBehaviour>(true).OfType<IChangeLevelText>().ToList();
        _changeLevelTexts.ForEach(i => i.ChangeLevelText(Level));
        if(_level > 12)
        {
            SpecialSpawner.Instance.AddSpecialType(SpecialType.Bomb);
            SpecialSpawner.Instance.AddSpecialType(SpecialType.Lightning);
        }
    }
}
