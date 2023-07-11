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
            _settingPopup.SetActive(false);
        });

        _homeButton.onClick.AddListener(delegate 
        {
            _homePanel.SetActive(true);
            _gamePanel.SetActive(false);
            _settingPopup.SetActive(false);
        });

        _rateButton.onClick.AddListener(delegate
        {

        });

        _soundButton.onClick.AddListener(delegate
        {

        });

        _vibrateButton.onClick.AddListener(delegate
        {

        });

        _tutorialButton.onClick.AddListener(delegate
        {

        });
    }
}
