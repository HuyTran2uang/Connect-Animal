using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarChestManager : MonoBehaviour
{
    [SerializeField] Button _backButton;
    [SerializeField] StarChestPopupUI _starChestPopup;

    private void Awake()
    {
        _backButton.onClick.AddListener(delegate
        {
            _starChestPopup.gameObject.SetActive(false);
            GameManager.Instance.Continue();
        });
    }
}
