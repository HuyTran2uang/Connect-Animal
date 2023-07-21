using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviourSingleton<TimerManager>, IPrepareGame
{
    private float _battleDuration, _totalTime = 180, _autoHintDuration, _timeHintConfig = 15, _nextShowInterAds;
    ProgressTimer _progressTimer;
    public float BattleDuration => _battleDuration;
    public float TotalTime => _totalTime;
    public float NextShowInterAds => _nextShowInterAds;

    public void Prepare()
    {
        _progressTimer = FindObjectOfType<ProgressTimer>(true);
    }

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
            _progressTimer?.CountDown(_battleDuration / 180);
        }
        else
            TimeOut();
    }

    public void SetNextShowInterAds()
    {
        _nextShowInterAds = 120;
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
        if (_nextShowInterAds > 0) _nextShowInterAds -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Timer();
        AutoHintTimer();
    }
}
