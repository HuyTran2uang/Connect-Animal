using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviourSingleton<StarManager>, IReadData, IPrepareGame
{
    private int _quantityStar;

    public void LoadData()
    {
        _quantityStar = Data.ReadData.LoadData(GlobalKey.STAR, 0);
    }

    public void Prepare()
    {
        //set ui
    }

    public void AddStar(int quantity)
    {
        _quantityStar += quantity;
        Data.WriteData.Save(GlobalKey.STAR, _quantityStar);
    }

    public void SubtractStar(int quantity)
    {
        _quantityStar -= quantity;
        Data.WriteData.Save(GlobalKey.STAR, _quantityStar);
    }
}
