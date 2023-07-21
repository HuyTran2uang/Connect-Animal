using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorePanel : MonoBehaviour
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
}
