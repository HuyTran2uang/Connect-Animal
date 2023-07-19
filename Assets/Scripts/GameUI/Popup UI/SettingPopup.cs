using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : MonoBehaviour
{
    [SerializeField] Button _resumeButton;
    [SerializeField] Button _homeButton;
    [SerializeField] Button _rateButton;
    [SerializeField] Button _tutorialButton;
    [SerializeField] Button _soundButton;
    [SerializeField] Button _vibrateButton;
    [SerializeField] Button _restartButton;
    [SerializeField] SettingPopup _settingPopup;
    [SerializeField] GamePanel _gamePanel;
    [SerializeField] HomePanel _homePanel;
    [SerializeField] RatePopupUI _ratePopup;

    private void Awake()
    {
        _resumeButton.onClick.AddListener(delegate 
        {
            AudioManager.Instance.PlaySoundClickButton();
            _settingPopup.gameObject.SetActive(false);
        });

        _homeButton.onClick.AddListener(delegate 
        {
            AudioManager.Instance.PlaySoundClickButton();
            _homePanel.gameObject.SetActive(true);
            _gamePanel.gameObject.SetActive(false);
            _settingPopup.gameObject.SetActive(false);
            GameManager.Instance.GoToMenuFromBattle();
        });

        _restartButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            ApplovinManager.Instance.ShowRewardedAd(delegate
            {
                _settingPopup.gameObject.SetActive(false);
                GameManager.Instance.Replay();
            });
        });

        _rateButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            _ratePopup.gameObject.SetActive(true);
        });

        _soundButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            AudioManager.Instance.Sound();
        });

        _vibrateButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            VibrationManager.Instance.Vibrate();
        });

        _tutorialButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            TutorialManager.Instance.StartTutorial();
            gameObject.SetActive(false);
        });
    }
    private void OnEnable()
    {
        GameManager.Instance.Wait();
        if (GameManager.Instance.GameState == GameState.None)
        {
            _rateButton.gameObject.SetActive(true);
            _restartButton.gameObject.SetActive(false);
        }
        else
        {
            _rateButton.gameObject.SetActive(false);
            _restartButton.gameObject.SetActive(true);
        }
    }
    private void OnDisable()
    {
        if (GameManager.Instance.GameState == GameState.None)
        {
            return;
        } 
        else
        {
            GameManager.Instance.ResumeGame();
        }
    }
}
