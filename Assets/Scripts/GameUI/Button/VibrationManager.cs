using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VibrationManager : MonoBehaviourSingleton<VibrationManager>, IReadData, IPrepareGame
{
    bool _isOpen;

    public void LoadData()
    {
        _isOpen = Data.ReadData.LoadData(GlobalKey.VIBRATION, true);
    }

    public void Prepare()
    {
        var setters = FindObjectsOfType<MonoBehaviour>(true).OfType<IVibrationButton>().ToList();
        setters.ForEach(i => i.SetState(_isOpen));
    }

    public void Vibrate()
    {
        _isOpen = !_isOpen;
        Data.WriteData.Save(GlobalKey.VIBRATION, _isOpen);
    }

    public void PlayVibrate()
    {
        if (!_isOpen) return;
        Handheld.Vibrate();
    }
}
