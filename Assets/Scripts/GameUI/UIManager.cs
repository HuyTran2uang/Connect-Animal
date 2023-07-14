using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviourSingleton<UIManager>
{
    [SerializeField] Button _settingButton;
    [SerializeField] HomePanel _homePanel;
    [SerializeField] GamePanel _gamePanel;
    [SerializeField] GameObject _winPopup;
    [SerializeField] GameObject _losePopup;
    [SerializeField] GameObject _settingPopup;

    private void Awake()
    {
        _settingButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            //show setting popup
            _settingPopup.SetActive(true);
        });
    }

    public void OnBattle()
    {
        _homePanel.gameObject.SetActive(false);
        _gamePanel.gameObject.SetActive(true);
    }

    public void GoToMenuFromBattle()
    {
        _homePanel.gameObject.SetActive(true);
        _gamePanel.gameObject.SetActive(false);
    }

    public void Win()
    {
        //show popup win
        _winPopup.SetActive(true);
    }

    public void Lose()
    {
        //show popup lose
        _losePopup.SetActive(true);
        if (TimerManager.Instance.TotalTime == 180)
        {
            _losePopup.GetComponent<LosePopup>().FirstLose();
        } else
        {
            _losePopup.GetComponent<LosePopup>().AfterFirstLose();
        }
    }
}
