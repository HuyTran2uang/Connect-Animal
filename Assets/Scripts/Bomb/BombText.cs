using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BombText : MonoBehaviour, IBombText
{
    [SerializeField] TMP_Text _quantityText;

    public void SetQuantityText(int quantity)
    {
        string quantityString = "";
        if (quantity >= 1000 && quantity < 1000000)
        {
            quantityString = $"{quantity / 1000}K";
        }
        else if (quantity >= 1000000 && quantity < 1000000000)
        {
            quantityString = $"{quantity / 1000000}M";
        }
        else if (quantity >= 1000000000)
        {
            quantityString = $"{quantity / 1000000000}B";
        }
        else
        {
            quantityString = quantity.ToString();
        }
        _quantityText.text = quantityString;
    }

    private void Reset()
    {
        _quantityText = GetComponent<TMP_Text>();
    }
}
