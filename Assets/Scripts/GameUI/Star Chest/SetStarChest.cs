using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetStarChest : MonoBehaviour
{
    [SerializeField] TMP_Text _star;
    int _currentStar, _maxStar;

    private void OnEnable()
    {
        _currentStar = StarManager.Instance.QuantityStar;
        _maxStar = ((_currentStar / 3000) + 1) * 3000;

        _star.text = _currentStar.ToString() + "/" + _maxStar.ToString();
    }

}
