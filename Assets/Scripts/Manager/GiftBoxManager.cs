using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GiftBoxManager : MonoBehaviourSingleton<GiftBoxManager>,IPrepareGame, IReadData
{
    float _timeReward = 600, _duration;
    bool _timeIsRunning = true;
    GiftBoxPopupUI _giftBoxPopup;

    public float Duration => _duration;

    public void LoadData()
    {
        _duration = Data.ReadData.LoadData(GlobalKey.GIFTBOXTIME, _timeReward);
    }

    public void Prepare()
    {
        _giftBoxPopup = FindObjectOfType<GiftBoxPopupUI>(true);
    }

    private void FixedUpdate()
    {
        if (_timeIsRunning)
        {
            if (_duration > 0)
            {
                _duration -= Time.deltaTime;
                _giftBoxPopup?.DisplayTime(_duration);
                _giftBoxPopup?.TimeRunning();
            }
            else
            {
                _duration = 0;
                _timeIsRunning = false;
                _giftBoxPopup?.TimeEnd();
            }
        }
        Data.WriteData.Save(GlobalKey.GIFTBOXTIME, _duration);
    }

    public void ResetTime()
    {
        _duration = _timeReward;
        _timeIsRunning = true;
    }
}
