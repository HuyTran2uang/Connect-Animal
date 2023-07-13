using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetCointText : MonoBehaviour, ISetCointText
{
    [SerializeField] List<TMP_Text> cointText = new List<TMP_Text>();

    void ISetCointText.SetCointText(int _coint)
    {
        foreach(TMP_Text coint in cointText)
        {
            coint.text = _coint.ToString();
        }
    }
}
