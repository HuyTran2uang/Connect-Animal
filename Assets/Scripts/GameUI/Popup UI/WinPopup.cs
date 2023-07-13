using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinPopup : MonoBehaviour
{
    [SerializeField] Button _homeButton;
    [SerializeField] Button _nextLevelButton;
    [SerializeField] GameObject _homePanel;
    [SerializeField] GameObject _gamePanel;
    [SerializeField] GameObject _winPopup;

    private void Awake()
    {
        _homeButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            _homePanel.SetActive(true);
            _gamePanel.SetActive(false);
            _winPopup.SetActive(false);
        });

        _nextLevelButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            _winPopup.SetActive(false);
            GameManager.Instance.Play();
        });
    }
}
