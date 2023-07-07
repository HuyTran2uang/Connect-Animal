using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeQuantityText : MonoBehaviour
{
    [SerializeField] TMP_Text _quantityText;

    public void SetQuantity(int quantity)
    {
        gameObject.SetActive(quantity == 0);
        _quantityText.text = quantity.ToString();
    }
}
