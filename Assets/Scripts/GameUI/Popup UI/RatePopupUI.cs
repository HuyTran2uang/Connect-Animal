using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatePopupUI : MonoBehaviour
{
    [SerializeField] Button _closeButton, _rateButton;

    private void Awake()
    {
        _closeButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            gameObject.SetActive(false);
        });

        _rateButton.onClick.AddListener(delegate
        {
            gameObject.SetActive(false);
            AudioManager.Instance.PlaySoundClickButton();
            EvaluateManager.Instance.Rate();
        });
    }
}
