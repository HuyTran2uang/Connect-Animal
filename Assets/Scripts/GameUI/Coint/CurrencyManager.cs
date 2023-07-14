using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CurrencyManager : MonoBehaviourSingleton<CurrencyManager>, IPrepareGame, IReadData
{
    List<ISetCointText> _SetCointTexts = new List<ISetCointText>();
    int _coint;
    public void LoadData()
    {
        _coint = Data.ReadData.LoadData(GlobalKey.COINT, 0);
    }

    public void Prepare()
    {
        //set ui
        _SetCointTexts = FindObjectsOfType<MonoBehaviour>(true).OfType<ISetCointText>().ToList();
        _SetCointTexts.ForEach(i => i.SetCointText(_coint));
    }

    public void AddCoint (int cointAdd)
    {
        _coint += cointAdd;
        Data.WriteData.Save(GlobalKey.COINT, _coint);
        _SetCointTexts.ForEach(i => i.SetCointText(_coint));
    }

    public bool SubtractCoint(int cointSubtract)
    {
        if (cointSubtract > _coint)
        {
            //TO DO
            return false;
        }
        else
        {
            _coint -= cointSubtract;
            Data.WriteData.Save(GlobalKey.COINT, _coint);
            _SetCointTexts.ForEach(i => i.SetCointText(_coint));

            return true;
        }
    }


}
