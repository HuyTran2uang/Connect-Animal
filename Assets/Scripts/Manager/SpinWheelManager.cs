using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWheelManager : MonoBehaviourSingleton<SpinWheelManager>, IPrepareGame, IReadData
{
    SpinWheelPopup _spinWheel;
    bool _isFirstSpined;
    float _15m = 15 * 60;

    public void LoadData()
    {
        _isFirstSpined = Data.ReadData.LoadData(GlobalKey.SPINED_LUCKY_WHEEL, false);
    }

    public void Prepare()
    {
        _spinWheel = FindObjectOfType<SpinWheelPopup>(true);
    }

    public void ActiveSpinWheel()
    {
        if (!_isFirstSpined) return;
        _spinWheel.gameObject.SetActive(true);
    }


    private void FixedUpdate()
    {
        if (!_isFirstSpined)
        {
            if (_15m > 0)
                _15m -= Time.deltaTime;
            else
            {
                Debug.Log("First show spin wheel");
                _isFirstSpined = true;
                Data.WriteData.Save(GlobalKey.SPINED_LUCKY_WHEEL, _isFirstSpined);
                ActiveSpinWheel();
            }
        }
    }
}
