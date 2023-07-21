using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class DayReward : MonoBehaviour
{
    [SerializeField] GameObject _cointIcon, _light, _check, _received, _unreceived;
    [SerializeField] TMP_Text _cointText, _dayText;
    [SerializeField] GameObject[] _icons;
    bool _isCollect;

    public void Init(DailyState state, int coint, int day)
    {
        if (day <= 3 )
        {
            _icons[0].SetActive(true);
        } 
        else if (day <= 6)
        {
            _icons[1].SetActive(true);
        }
        else
        {
            _icons[2].SetActive(true);
        }

        switch (state)
        {
            case DailyState.IsClose:
                IsClose();
                break;
            case DailyState.IsSelect:
                IsSelect(); 
                break;
            case DailyState.IsOpened: 
                IsOpened(); 
                break;
        }
        _dayText.text = day.ToString("DAY " + day);
        _cointText.text = coint.ToString();
    }

    public void IsClose()
    {
        _cointIcon.SetActive(true);
        _light.SetActive(false);
        _check.SetActive(false);
        _received.SetActive(true);
        _unreceived.SetActive(false);
    }
    public void IsSelect()
    {
        _cointIcon.SetActive(true);
        _light.SetActive(true);
        _check.SetActive(false);
        _received.SetActive(false);
        _unreceived.SetActive(true);
    }

    public void IsOpened()
    {
        _cointIcon.SetActive(false);
        _light.SetActive(false);
        _check.SetActive(true);
        _received.SetActive(true);
        _unreceived.SetActive(false);
    }


}

public enum DailyState
{
    IsClose,
    IsSelect, 
    IsOpened
}