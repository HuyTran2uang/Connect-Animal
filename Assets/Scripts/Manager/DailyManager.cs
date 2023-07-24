using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyManager : MonoBehaviourSingleton<DailyManager>, IAfterPrepareGame, IReadData, IPrepareGame
{
    DateTime _currentDate = DateTime.Now, _lastDate;
    int _lastDay, _lastMonth, _lastYear;
    int[] _cointDayReward = { 100, 150, 200, 250, 300, 350, 400, 450, 500 };
    int _dayCount;

    DailyPopup _dailyPopup;

    public int DayCount => _dayCount;
    public int[] CointDayReward => _cointDayReward;

    public void AfterPrepareGame()
    {
        if (LevelManager.Instance.Level > 2)
        {
            if (_lastYear != _currentDate.Year || _lastMonth != _currentDate.Month || _lastDay != _currentDate.Day)
            {
                ActiveDailyReward();
                SpinWheelManager.Instance.ActiveSpinWheel();
            }
        }
    }

    public void LoadData()
    {   
        _dayCount = Data.ReadData.LoadData(GlobalKey.DAYCOUNT, _dayCount);
        _lastDay = Data.ReadData.LoadData(GlobalKey.LASTDAY, _lastDate.Day);
        _lastMonth = Data.ReadData.LoadData(GlobalKey.LASTMONTH, _lastDate.Month);
        _lastYear = Data.ReadData.LoadData(GlobalKey.LASTYEAR, _lastDate.Year);
    }

    public void SelectReward()
    {
        _dayCount++;
        Data.WriteData.Save(GlobalKey.DAYCOUNT, _dayCount);
        _currentDate =  DateTime.Now;
        Data.WriteData.Save(GlobalKey.LASTDAY, _currentDate.Day);
        Data.WriteData.Save(GlobalKey.LASTMONTH, _currentDate.Month);
        Data.WriteData.Save(GlobalKey.LASTYEAR, _currentDate.Year);
    }

    public void Prepare()
    {
        _dailyPopup = FindObjectOfType<DailyPopup>(true);
    }

    public void ActiveDailyReward()
    {
        _dailyPopup?.gameObject.SetActive(true);
        _dailyPopup?.SetListItemUI(_cointDayReward, _dayCount);
    }
}
