using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinPopup : MonoBehaviour
{
    [SerializeField] Button _homeButton;
    [SerializeField] Button _nextLevelButton;
    [SerializeField] HomePanel _homePanel;
    [SerializeField] GamePanel _gamePanel;
    [SerializeField] WinPopup _winPopup;

    private void Awake()
    {
        _homeButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            _homePanel.gameObject.SetActive(true);
            _gamePanel.gameObject.SetActive(false);
            _winPopup.gameObject.SetActive(false);
        });

        _nextLevelButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            _winPopup.gameObject.SetActive(false);
            GameManager.Instance.Play();
        });
    }

    private void OnEnable()
    {
        if (LevelManager.Instance.Level < 2) return;
        Debug.Log("Show MREC");
        ApplovinManager.Instance.HideBanner();
        ApplovinManager.Instance.ShowMRec();
    }

    private void OnDisable()
    {
        if (LevelManager.Instance.Level < 2) return;
        ApplovinManager.Instance.HideMRec();
        ApplovinManager.Instance.ShowBanner();
    }
}
