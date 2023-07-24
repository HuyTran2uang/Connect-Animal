using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CointText : MonoBehaviour, ICointText
{
    [SerializeField] TMP_Text _cointText;

    private void Reset()
    {
        _cointText = GetComponent<TMP_Text>();
    }

    public void SetCointText(int coint)
    {
        string cointString = "";
        if(coint >= 1000 && coint < 1000000)
        {
            cointString = $"{coint / 1000}K";
        }
        else if(coint >= 1000000 && coint < 1000000000)
        {
            cointString = $"{coint / 1000000}M";
        }
        else if(coint >= 1000000000)
        {
            cointString = $"{coint / 1000000000}B";
        }
        else
        {
            cointString = coint.ToString();
        }
        _cointText.text = cointString;
    }
}
