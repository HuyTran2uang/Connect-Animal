using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviourSingleton<TimerManager>
{
    private float _battleDuration, _totalTime = 180, _autoHintDuration, _timeHintConfig = 15;

    public float BattleDuration => _battleDuration;
    public float TotalTime => _totalTime;

    public void SetTimer(float seconds)
    {
        _battleDuration = seconds;
    }

    public void ResetTotalTime()
    {
        _totalTime = 180;
    }

    public void IncreaseTime(int seconds)
    {
        _battleDuration += seconds;
        _totalTime += seconds;
    }

    public void ResetAutoHintTimer()
    {
        if (LevelManager.Instance.Level > 5) return;
        _autoHintDuration = _timeHintConfig;
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

    private void AutoHintTimer()
    {
        if (GameManager.Instance.GameState != GameState.OnBattle) return;
        if (GameManager.Instance.BattleState != BattleState.None) return;
        if (LevelManager.Instance.Level > 5) return;
        if (_autoHintDuration > 0)
            _autoHintDuration -= Time.deltaTime;
        else
            HintManager.Instance.HintFree();
    }

    private void FixedUpdate()
    {
        Timer();
        AutoHintTimer();
    }
}
