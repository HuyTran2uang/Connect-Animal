using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetLevelChest : MonoBehaviour
{
    [SerializeField] TMP_Text _level;

    int _levelCurrent, _levelMax;

    private void OnEnable()
    {
        _levelCurrent = LevelManager.Instance.Level;
        _levelMax = ((_levelCurrent / 10) + 1) * 10;

        _level.text = _levelCurrent.ToString() + "/" + _levelMax.ToString();
    }
}
