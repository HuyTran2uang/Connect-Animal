using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;

public class GameManager : MonoBehaviourSingleton<GameManager>, IPrepareGame
{
    private GameState _gameState;
    private BattleState _battleState;

    public GameState GameState => _gameState;
    public BattleState BattleState => _battleState;
    GamePanel _gamePanel;

    public void Prepare()
    {
        _gamePanel = FindObjectOfType<GamePanel>(true);
    }

    public void Play()
    {
        if(!TutorialManager.Instance.IsPassedLevelTutorial)
        {
            Debug.Log("Level tutorial");
            TutorialManager.Instance.OpenLevelTutorial();
            return;
        }
        if (LevelManager.Instance.Level == 32)
        {
            EvaluateManager.Instance.Open();
        }
        Wait();
        _gameState = GameState.OnBattle;
        TimerManager.Instance.SetTimer(180);
        StarManager.Instance.ClearStarInLevel();
        BoardManager.Instance.CreateBoard(LevelConfigConverter.GetLevelConfig(LevelManager.Instance.Level));
        _gamePanel.SetLevelPlaying(LevelManager.Instance.Level);
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
        _gameState = GameState.OnBattle;
        _battleState = BattleState.None;
    }

    public void Continue()
    {
        Wait();
        BoardManager.Instance.Clear();
        Play();
    }

    public void Replay()
    {
        Wait();
        BoardManager.Instance.Clear();
        Play();
    }

    public void Win()
    {
        _gameState = GameState.None;
        Wait();
        AudioManager.Instance.PlaySoundWinButton();
        LevelManager.Instance.LevelUp();
        if (LevelManager.Instance.Level == 2) DailyManager.Instance.ActiveDailyReward();
        if (LevelManager.Instance.Level % 10 == 0)
        {
            ChestManager.Instance.OpenLevelChest();
        }
        else
        {
            UIManager.Instance.Win();
        }
        StarManager.Instance.PassStarInLevelToData();
        TimerManager.Instance.ResetTotalTime();
    }

    public void Lose()
    {
        _gameState = GameState.None;
        Wait();
        AudioManager.Instance.PlaySoundLoseButton();
        UIManager.Instance.Lose();
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
