using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviourSingleton<UIManager>
{
    [SerializeField] Button _settingButton;
    [SerializeField] HomePanel _homePanel;
    [SerializeField] GamePanel _gamePanel;

    private void Awake()
    {
        _settingButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            //show setting popup
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
    }

    public void Lose()
    {
        //show popup lose
    }
}
