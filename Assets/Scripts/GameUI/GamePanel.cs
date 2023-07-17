using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    [SerializeField] Button _bombButton, _hintButton, _remapButton, _coinButton;
    [SerializeField] StorePanel _storePanel;
    [SerializeField] NotEnoughCoinPopup _notEnoughCoinPopup;

    private void Awake()
    {
        _bombButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            if(!BombManager.Instance.Throw())
                _notEnoughCoinPopup.gameObject.SetActive(true);
        });

        _hintButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            if(!HintManager.Instance.Hint())
                _notEnoughCoinPopup.gameObject.SetActive(true);
        });

        _remapButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            if(!RemapManager.Instance.Remap())
                _notEnoughCoinPopup.gameObject.SetActive(true);
        });

        _coinButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            _storePanel.gameObject.SetActive(true);
            GameManager.Instance.Wait();
        });
    }
}
