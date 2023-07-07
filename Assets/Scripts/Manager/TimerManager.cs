using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviourSingleton<TimerManager>
{
    private float _battleDuration;

    public void SetTimer(int seconds)
    {
        _battleDuration = seconds;
    }

    public void IncreaseTime(int seconds)
    {
        _battleDuration += seconds;
    }

    private void TimeOut()
    {
        GameManager.Instance.Lose();
    }

    private void Timer()
    {
        if (GameManager.Instance.BattleState != BattleState.None) return;
        if (_battleDuration > 0)
            _battleDuration -= Time.deltaTime;
        else
            TimeOut();
    }

    private void FixedUpdate()
    {
        Timer();
    }
}
