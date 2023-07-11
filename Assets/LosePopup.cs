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
    [SerializeField] Button _homeButton;
    [SerializeField] Button _restartButton;
    [SerializeField] Button _restartButton2;
    [SerializeField] Button _watchAdButton;
    [SerializeField] Button _cointPlayButton;
    [SerializeField] Button _cointPlayButton2;
    [SerializeField] TMP_Text coint;

    private void Awake()
    {
        _homeButton.onClick.AddListener(delegate
        {
            _homePanel.SetActive(true);
            _gamePanel.SetActive(false);
            _losePopup.SetActive(false);
        });

        _restartButton.onClick.AddListener(delegate
        {

        });

        _restartButton2.onClick.AddListener(delegate
        {

        });

        _watchAdButton.onClick.AddListener(delegate
        {

        });

        _cointPlayButton.onClick.AddListener(delegate
        {

        });

        _cointPlayButton2.onClick.AddListener(delegate
        {

        });
    }
}
