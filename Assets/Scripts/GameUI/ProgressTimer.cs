using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressTimer : MonoBehaviour
{
    [SerializeField] Image _timeProgress;

    public void CountDown(float rate)
    {
        if (rate > 0.66f)
        {
            _timeProgress.color = new Color32(30, 255, 30, 255);
        } 
        if (rate < 0.33f)
        {
            _timeProgress.color = new Color32(245,36,12, 255);
        }
        if (rate < 0.66f && rate >= 0.33f)
        {
            _timeProgress.color = new Color32(245, 160, 12, 255);
        }
        _timeProgress.fillAmount = rate;
    }
}
