using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageUI : MonoBehaviour
{
    [SerializeField] Button _buyButton;

    private void Awake()
    {
        _buyButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            OnBuyPackage();
        });
    }

    protected virtual void OnBuyPackage()
    {
        //to override
    }
}
