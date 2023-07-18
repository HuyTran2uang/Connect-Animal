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
    [SerializeField] GameObject _settingPopup;
    [SerializeField] GameObject _gamePanel;
    [SerializeField] GameObject _homePanel;

    private void Awake()
    {
        _resumeButton.onClick.AddListener(delegate 
        {
            AudioManager.Instance.PlaySoundClickButton();
            _settingPopup.SetActive(false);
        });

        _homeButton.onClick.AddListener(delegate 
        {
            AudioManager.Instance.PlaySoundClickButton();
            _homePanel.SetActive(true);
            _gamePanel.SetActive(false);
            _settingPopup.SetActive(false);
            BoardManager.Instance.Clear();
        });

        _rateButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();

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
        });
    }
    private void OnEnable()
    {
        GameManager.Instance.Wait();
    }
    private void OnDisable()
    {
        GameManager.Instance.ResumeGame();
    }
}
