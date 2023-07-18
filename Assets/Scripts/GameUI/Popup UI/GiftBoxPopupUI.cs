using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GiftBoxPopupUI : MonoBehaviour
{
    [SerializeField] Button _rewardButton;
    [SerializeField] Button _backButton;
    [SerializeField] GameObject _giftBoxPopup, _light;

    [SerializeField] TMP_Text _timerIn, _timerOut;
    [SerializeField] TMP_Text _rewardAds, _rewardAdsText;

    private void Awake()
    {
        _rewardButton.onClick.AddListener(delegate
        {
            ApplovinManager.Instance.ShowRewardedAd(delegate
            {
                BombManager.Instance.AddThrowTimes(1);
                HintManager.Instance.AddHintTimes(1);
                RemapManager.Instance.AddRemapTimes(1);
                GiftBoxManager.Instance.ResetTime();
            });
        });

        _backButton.onClick.AddListener(delegate
        {
            _giftBoxPopup.SetActive(false);
        });
    }

    public void DisplayTime(float _timeToDisplay)
    {
        _timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(_timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(_timeToDisplay % 60);

        _timerIn.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        _timerOut.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void TimeRunning()
    {
        _rewardButton.interactable = false;
        _timerIn.gameObject.SetActive(true);
        _timerOut.gameObject.SetActive(true);

        _light.SetActive(false);
        _rewardAds.gameObject.SetActive(false);
        _rewardAdsText.gameObject.SetActive(false);
    }

    public void TimeEnd()
    {
        _rewardButton.interactable = true;
        _timerIn.gameObject.SetActive(false);
        _timerOut.gameObject.SetActive(false);

        _light.SetActive(true);
        _rewardAds.gameObject.SetActive(true);
        _rewardAdsText.gameObject.SetActive(true);
    }
}
