using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintText : MonoBehaviour, IHintText
{
    [SerializeField] TMP_Text _quantity;

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
        _quantity.text = quantityString;
    }

    private void Reset()
    {
        _quantity = GetComponent<TMP_Text>();
    }
}
