using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    private GameState _gameState;
    private BattleState _battleState;

    public GameState GameState => _gameState;
    public BattleState BattleState => _battleState;

    public void Play()
    {
        Wait();
        _gameState = GameState.OnBattle;
        TimerManager.Instance.SetTimer(180);
        StarManager.Instance.ClearStarInLevel();
        BoardManager.Instance.CreateBoard(LevelConfigConverter.GetLevelConfig(LevelManager.Instance.Level));
        ResumeGame();
    }

    public void GoToMenuFromBattle()
    {
        _gameState = GameState.None;
        BoardManager.Instance.Clear();
        HintManager.Instance.UnHint();
    }

    public void Wait()
    {
        _battleState = BattleState.Wait;
    }

    public void ResumeGame()
    {
        _battleState = BattleState.None;
    }

    public void Replay()
    {
        Play();
    }

    public void Win()
    {
        _gameState = GameState.None;
        Wait();
        AudioManager.Instance.PlaySoundWinButton();
        LevelManager.Instance.LevelUp();
        StarManager.Instance.PassStarInLevelToData();
        UIManager.Instance.Win();
        Debug.Log("Win");
    }

    public void Lose()
    {
        _gameState = GameState.None;
        Wait();
        AudioManager.Instance.PlaySoundLoseButton();
        UIManager.Instance.Lose();
        Debug.Log("Lose");
    }
}

public enum GameState
{
    None,
    OnBattle,
}

public enum BattleState
{
    None,
    Wait,
}
