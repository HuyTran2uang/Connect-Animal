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

    public void LoadData()
    {
        _dayCount = Data.ReadData.LoadData(GlobalKey.DAYCOUNT, _dayCount);
        _lastDay = Data.ReadData.LoadData(GlobalKey.LASTDAY, _lastDate.Day);
        _lastMonth = Data.ReadData.LoadData(GlobalKey.LASTMONTH, _lastDate.Month);
        _lastYear = Data.ReadData.LoadData(GlobalKey.LASTYEAR, _lastDate.Year);
    }

    public void Prepare()
    {
        _dailyPopup = FindObjectOfType<DailyPopup>(true);
    }

    public void AfterPrepareGame()
    {
        if (LevelManager.Instance.Level > 2)
        {
            if (IsNewDay())
            {
                ActiveDailyReward();
                SpinWheelManager.Instance.ActiveSpinWheel();
                SaveToday();
            }
        }
    }

    private bool IsNewDay()
    {
        return _lastYear != _currentDate.Year || _lastMonth != _currentDate.Month || _lastDay != _currentDate.Day;
    }

    public void SelectReward()
    {
        _dayCount++;
        Data.WriteData.Save(GlobalKey.DAYCOUNT, _dayCount);
    }

    private void SaveToday()
    {
        Data.WriteData.Save(GlobalKey.LASTDAY, DateTime.Now.Day);
        Data.WriteData.Save(GlobalKey.LASTMONTH, DateTime.Now.Month);
        Data.WriteData.Save(GlobalKey.LASTYEAR, DateTime.Now.Year);
    }

    public void ActiveDailyReward()
    {
        _dailyPopup?.gameObject.SetActive(true);
        _dailyPopup?.SetListItemUI(_cointDayReward, _dayCount);
    }
}
