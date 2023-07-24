using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StarManager : MonoBehaviourSingleton<StarManager>, IReadData, IPrepareGame
{
    [SerializeField] private int _quantityStar, _quantityStarInLevel;
    List<IStarText> _starTexts = new List<IStarText>();
    List<IStarLevelText> _starLevelTexts = new List<IStarLevelText>();

    public int QuantityStar => _quantityStar;

    public void LoadData()
    {
        _quantityStar = Data.ReadData.LoadData(GlobalKey.STAR, 0);
    }

    public void Prepare()
    {
        _starTexts = FindObjectsOfType<MonoBehaviour>(true).OfType<IStarText>().ToList();
        _starTexts.ForEach(i => i.SetQuantityText(_quantityStar));
        _starLevelTexts = FindObjectsOfType<MonoBehaviour>(true).OfType<IStarLevelText>().ToList();
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
        _starLevelTexts.ForEach(i => i.SetQuantityText(_quantityStarInLevel));
    }

    public void PassStarInLevelToData()
    {
        AddStarData(_quantityStarInLevel);
    }

    public void ClearStarInLevel()
    {
        _quantityStarInLevel = 0;
        _starLevelTexts.ForEach(i => i.SetQuantityText(_quantityStarInLevel));
    }
}
