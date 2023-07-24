using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWheelManager : MonoBehaviourSingleton<SpinWheelManager>, IPrepareGame
{
    SpinWheelPopup _spinWheel;
    public void Prepare()
    {
        _spinWheel = FindObjectOfType<SpinWheelPopup>(true);
    }

    public void ActiveSpinWheel()
    {
        _spinWheel.gameObject.SetActive(true);
    }

}
