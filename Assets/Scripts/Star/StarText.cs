using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarText : MonoBehaviour, IStarText
{
    [SerializeField] TMP_Text _quantityText;

    public void SetQuantityText(int quantity)
    {
        _quantityText.text = quantity.ToString();
    }

    private void Reset()
    {
        _quantityText = GetComponent<TMP_Text>();
    }
}
