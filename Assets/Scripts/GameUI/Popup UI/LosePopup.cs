using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LosePopup : MonoBehaviour
{
    [SerializeField] GameObject _losePopup;
    [SerializeField] GameObject _homePanel;
    [SerializeField] GameObject _gamePanel;
    [SerializeField] GameObject _playCointButton, _adCoint;
    [SerializeField] Button _homeButton;
    [SerializeField] Button _restartButton;
    [SerializeField] Button _restartButton2;
    [SerializeField] Button _watchAdButton;
    [SerializeField] Button _cointPlayButton;
    [SerializeField] Button _cointPlayButton2;
    [SerializeField] TMP_Text coint;
    [SerializeField] NotEnoughCoinPopup _notEnoughCoinPopup;

    private void Awake()
    {
        _homeButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            _homePanel.SetActive(true);
            _gamePanel.SetActive(false);
            _losePopup.SetActive(false);
            GameManager.Instance.GoToMenuFromBattle();
            BoardManager.Instance.Clear();
        });

        _restartButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            ApplovinManager.Instance.ShowInterstitial();
            _losePopup.SetActive(false);
            TimerManager.Instance.ResetTotalTime();
            GameManager.Instance.Replay();
        });

        _restartButton2.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            ApplovinManager.Instance.ShowInterstitial();
            _losePopup.SetActive(false);
            TimerManager.Instance.ResetTotalTime();
            GameManager.Instance.Replay();
        });

        _watchAdButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            ApplovinManager.Instance.ShowRewardedAd(delegate
            {
                TimerManager.Instance.SetTimer(60);
                GameManager.Instance.ResumeGame();
                _losePopup.SetActive(false);
            });
        });

        _cointPlayButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            if (CurrencyManager.Instance.SubtractCoint(300)) 
            {
                TimerManager.Instance.SetTimer(60);
                GameManager.Instance.ResumeGame();
                _losePopup.SetActive(false);
            }
            else
            {
                _notEnoughCoinPopup.gameObject.SetActive(true);
            }
        });

        _cointPlayButton2.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            if (CurrencyManager.Instance.SubtractCoint(500)) 
            {
                TimerManager.Instance.SetTimer(60);
                GameManager.Instance.ResumeGame();
                _losePopup.SetActive(false);
            }
            else
            {
                _notEnoughCoinPopup.gameObject.SetActive(true);
            }
        });
    }

    public void FirstLose()
    {
        _adCoint.SetActive(true);
        _playCointButton.SetActive(false);
    }

    public void AfterFirstLose()
    {
        _adCoint.SetActive(false);
        _playCointButton.SetActive(true);
    }
}
