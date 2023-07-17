using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BombText : MonoBehaviour, IBombText
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
