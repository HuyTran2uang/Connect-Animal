using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrationButton : Switch2Phase, IVibrationButton
{
    [SerializeField] Button _vibrationButton;

    public void SetState(bool state)
    {
        if (state) this.On(); else this.Off();
    }

    private void Awake()
    {
        _vibrationButton.onClick.AddListener(delegate
        {
            this.SwitchPhase();
        });
    }
}
