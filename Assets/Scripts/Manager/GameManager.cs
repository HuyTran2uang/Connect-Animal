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
        _gameState = GameState.OnBattle;
        _battleState = BattleState.None;
        TimerManager.Instance.SetTimer(180);
        StarManager.Instance.ClearStarInLevel();
        BoardManager.Instance.CreateBoard(LevelConfigStorage.LevelConfigs[LevelManager.Instance.Level]);
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
        Wait();
        BoardManager.Instance.CreateBoard(LevelConfigStorage.LevelConfigs[LevelManager.Instance.Level]);
        ResumeGame();
    }

    public void Win()
    {
        _gameState = GameState.None;
        Wait();
        AudioManager.Instance.PlaySoundWinButton();
        LevelManager.Instance.LevelUp();
        StarManager.Instance.PassStarInLevelToData();
        Debug.Log("Win");
    }

    public void Lose()
    {
        _gameState = GameState.None;
        Wait();
        AudioManager.Instance.PlaySoundLoseButton();
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
