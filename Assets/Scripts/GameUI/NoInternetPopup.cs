using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NoInternetPopup : MonoBehaviour
{
    [SerializeField] Button _retryConnectButton;

    private void Awake()
    {
        _retryConnectButton.onClick.AddListener(delegate
        {
            gameObject.SetActive(false);
            NetworkManager.Instance.RetryConnectInternet();
        });
    }
}
