using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviourSingleton<TimerManager>
{
    private float _battleDuration;
    private float totalTime = 180;
    public float BattleDuration => _battleDuration;
    public float TotalTime => totalTime;

    public void SetTimer(float seconds)
    {
        _battleDuration = seconds;
    }

    public void ResetTotalTime()
    {
        totalTime = 180;
    }

    public void IncreaseTime(int seconds)
    {
        _battleDuration += seconds;
        totalTime += seconds;
    }

    private void TimeOut()
    {
        GameManager.Instance.Lose();
    }

    private void Timer()
    {
        if (GameManager.Instance.GameState != GameState.OnBattle) return;
        if (GameManager.Instance.BattleState != BattleState.None) return;
        if (_battleDuration > 0)
        {
            _battleDuration -= Time.deltaTime;
            TimeUI.Instance.CountDown(_battleDuration / 180);
        }
        else
            TimeOut();
    }

    private void FixedUpdate()
    {
        Timer();
    }
}
