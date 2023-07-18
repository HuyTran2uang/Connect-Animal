using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluateManager : MonoBehaviourSingleton<EvaluateManager>, IReadData, IPrepareGame
{
    private bool _isEvaluated;
    RatePopupUI _ratePopup;

    public void LoadData()
    {
        _isEvaluated = Data.ReadData.LoadData(GlobalKey.EVALUATE, false);
    }

    public void Prepare()
    {
        _ratePopup = FindObjectOfType<RatePopupUI>(true);
    }

    public void Open()
    {
        _ratePopup.gameObject.SetActive(true);
    }

    public void Rate()
    {
        _isEvaluated = true;
        Data.WriteData.Save(GlobalKey.EVALUATE, _isEvaluated);
        Application.OpenURL("market://details?id=" + Application.identifier);
    }
}
