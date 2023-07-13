using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch2Phase : MonoBehaviour
{
    [SerializeField] protected GameObject _active;
    [SerializeField] protected GameObject _deActive;
    bool _isOn;

    protected void Off()
    {
        _isOn = false;
        _active.SetActive(false);
        _deActive.SetActive(true);
    }

    protected void On()
    {
        _isOn = true;
        _active.SetActive(true);
        _deActive.SetActive(false);
    }

    protected void SwitchPhase()
    {
        if (_isOn)
            Off();
        else
            On();
    }
}
