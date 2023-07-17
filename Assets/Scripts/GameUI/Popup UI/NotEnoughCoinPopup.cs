using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotEnoughCoinPopup : MonoBehaviour
{
    [SerializeField] Button _closeButton;

    private void Awake()
    {
        _closeButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            gameObject.SetActive(false);
        });
    }

    private void OnEnable()
    {
        GameManager.Instance.Wait();
    }

    private void OnDisable()
    {
        GameManager.Instance.ResumeGame();
    }
}
