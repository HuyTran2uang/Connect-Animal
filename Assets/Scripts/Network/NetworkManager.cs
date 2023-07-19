using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviourSingleton<NetworkManager>, IPrepareGame
{
    NoInternetPopup _noInternetPopup;

    public bool CheckHasInternet()
    {
        if(Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("No internet");
            if (_noInternetPopup == null)
                _noInternetPopup = FindObjectOfType<NoInternetPopup>(true);
            if (_noInternetPopup.isActiveAndEnabled == false)
            {
                _noInternetPopup.gameObject.SetActive(true);
            }
            return false;
        }
        return true;
    }

    public void Prepare()
    {
        _noInternetPopup = FindObjectOfType<NoInternetPopup>(true);
    }

    public bool RetryConnectInternet()
    {
        return CheckHasInternet();
    }

    private void Update()
    {
        CheckHasInternet();
    }
}
