using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarLevelText : MonoBehaviour, IStarLevelText
{
    [SerializeField] TMP_Text _quantityText;

    private void Reset()
    {
        _quantityText = GetComponent<TMP_Text>();
    }

    public void SetQuantityText(int quantity)
    {
        _quantityText.text = quantity.ToString();
    }
}
