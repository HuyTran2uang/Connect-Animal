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
        _cointText.text = coint.ToString();
    }
}
