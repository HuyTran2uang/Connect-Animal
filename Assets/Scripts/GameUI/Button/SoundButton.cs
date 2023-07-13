using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : Switch2Phase, ISoundButton
{
    [SerializeField] Button _soundButton;

    public void SetState(bool state)
    {
        if (state) this.On(); else this.Off();
    }

    private void Awake()
    {
        _soundButton.onClick.AddListener(delegate
        {
            this.SwitchPhase();
        });
    }
}
