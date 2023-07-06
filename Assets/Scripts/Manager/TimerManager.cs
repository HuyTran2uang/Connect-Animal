using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private int _battleTime, _15Second;

    public void SetTimer(int seconds)
    {
        _battleTime = seconds;
    }


}
