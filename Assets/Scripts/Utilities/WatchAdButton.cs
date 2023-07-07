using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WatchAdButton : MonoBehaviour, ILoadRewardAd
{
    [SerializeField] Button _button;

    private void Reset()
    {
        _button = GetComponent<Button>();
    }

    public void LoadRewardAd(bool hasRewardAd)
    {
        _button.interactable = hasRewardAd;
    }
}
