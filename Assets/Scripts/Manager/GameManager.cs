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
        BoardManager.Instance.CreateBoard(LevelConfigStorage.LevelConfigs[LevelManager.Instance.Level]);
    }

    public void GoToMenuFromBattle()
    {
        _gameState = GameState.None;
        BoardManager.Instance.Clear();
    }

    public void PauseGame()
    {
        _battleState = BattleState.Pausing;
        //pause timer
    }

    public void ResumeGame()
    {
        _battleState = BattleState.None;
        //resume timer
    }

    public void Replay()
    {
        _battleState = BattleState.Replaying;
        //completed re ren board
        _battleState = BattleState.None;
    }

    public void CheckingConnection()
    {
        _battleState = BattleState.CheckingConnection;
    }

    public void CompletedCheckConnection()
    {
        _battleState = BattleState.None;
    }

    public void Remap()
    {
        _battleState = BattleState.Remap;
    }

    public void CompletedRemap()
    {
        _battleState = BattleState.None;
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
    Pausing,
    Replaying,
    CheckingConnection,
    Remap,
}
