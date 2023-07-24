using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePanel : MonoBehaviour
{
    [SerializeField] Button _bombButton, _hintButton, _remapButton, _coinButton, _giftBoxButton;
    [SerializeField] StorePanel _storePanel;
    [SerializeField] NotEnoughCoinPopup _notEnoughCoinPopup;
    [SerializeField] GiftBoxPopupUI _giftBoxPopup;
    [SerializeField] TMP_Text _levelText;

    private void Awake()
    {
        _bombButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            BombManager.Instance.Throw();
        });

        _hintButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            HintManager.Instance.Hint();
        });

        _remapButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            RemapManager.Instance.Remap();
        });

        _coinButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            _storePanel.gameObject.SetActive(true);
        });

        _giftBoxButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            _giftBoxPopup.gameObject.SetActive(true);
        });
    }

    public void SetLevelPlaying(int level)
    {
        _levelText.text = $"Level {level}";
    }
}
