using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintText : MonoBehaviour, IHintText
{
    [SerializeField] TMP_Text _quantity;

    public void SetQuantityText(int quantity)
    {
        _quantity.text = quantity.ToString();
    }

    private void Reset()
    {
        _quantity = GetComponent<TMP_Text>();
    }
}
