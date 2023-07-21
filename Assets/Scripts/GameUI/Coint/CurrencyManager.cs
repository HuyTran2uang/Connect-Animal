using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CurrencyManager : MonoBehaviourSingleton<CurrencyManager>, IPrepareGame, IReadData
{
    List<ICointText> _setCointTexts = new List<ICointText>();
    int _coint;
    NotEnoughCoinPopup _notEnoughCoinPopup;

    public void LoadData()
    {
        _coint = Data.ReadData.LoadData(GlobalKey.COINT, 0);
    }

    public void Prepare()
    {
        _notEnoughCoinPopup = FindObjectOfType<NotEnoughCoinPopup>(true);
        _setCointTexts = FindObjectsOfType<MonoBehaviour>(true).OfType<ICointText>().ToList();
        _setCointTexts.ForEach(i => i.SetCointText(_coint));
    }

    public void AddCoint (int cointAdd)
    {
        _coint += cointAdd;
        Data.WriteData.Save(GlobalKey.COINT, _coint);
        _setCointTexts.ForEach(i => i.SetCointText(_coint));
    }

    public bool SubtractCoint(int cointSubtract)
    {
        if (cointSubtract > _coint)
        {
            //TO DO
            _notEnoughCoinPopup.gameObject.SetActive(true);
            return false;
        }
        else
        {
            _coint -= cointSubtract;
            Data.WriteData.Save(GlobalKey.COINT, _coint);
            _setCointTexts.ForEach(i => i.SetCointText(_coint));

            return true;
        }
    }


}
