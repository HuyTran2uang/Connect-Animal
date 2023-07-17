using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomePanel : MonoBehaviour
{
    [SerializeField] Button _playButton, _coinButton;
    [SerializeField] StorePanel _storePanel;

    private void Awake()
    {
        _playButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            GameManager.Instance.Play();
            UIManager.Instance.OnBattle();
        });
        _coinButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            _storePanel.gameObject.SetActive(true);
        });
    }
}
