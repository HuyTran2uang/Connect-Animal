using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StarManager : MonoBehaviourSingleton<StarManager>, IReadData, IPrepareGame
{
    [SerializeField] private int _quantityStar, _quantityStarInLevel;
    List<IChangeStarText> _changeStarTexts = new List<IChangeStarText>();
    List<IStarText> _starTexts = new List<IStarText>();

    public void LoadData()
    {
        _quantityStar = Data.ReadData.LoadData(GlobalKey.STAR, 0);
    }

    public void Prepare()
    {
        _changeStarTexts = FindObjectsOfType<MonoBehaviour>(true).OfType<IChangeStarText>().ToList();
        _changeStarTexts.ForEach(i => i.ChangeStarText(_quantityStar));
        _starTexts = FindObjectsOfType<MonoBehaviour>(true).OfType<IStarText>().ToList();
        _starTexts.ForEach(i => i.SetQuantityText(_quantityStar));
    }

    private void AddStarData(int quantity)
    {
        _quantityStar += quantity;
        Data.WriteData.Save(GlobalKey.STAR, _quantityStar);
        _starTexts.ForEach(i => i.SetQuantityText(_quantityStar));
    }

    public void AddStar(int quantity)
    {
        _quantityStarInLevel += quantity;
        _changeStarTexts.ForEach(i => i.ChangeStarTextInLevel(_quantityStarInLevel));
    }

    public void PassStarInLevelToData()
    {
        AddStarData(_quantityStarInLevel);
    }

    public void ClearStarInLevel()
    {
        _quantityStarInLevel = 0;
        _changeStarTexts.ForEach(i => i.ChangeStarTextInLevel(_quantityStarInLevel));
    }
}
