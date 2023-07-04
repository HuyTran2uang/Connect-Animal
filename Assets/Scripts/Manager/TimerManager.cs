using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private int _duration;

    public void SetTimer(int seconds)
    {
        _duration = seconds;
    }
}
