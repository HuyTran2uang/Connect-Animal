using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RemapUI : MonoBehaviourSingleton<RemapUI>
{
    [SerializeField] ChangeQuantityText _changeQuantityText;
    [SerializeField] GameObject _coinPanel;

    private void Awake()
    {
        _coinPanel.transform.Find("Quantity Text").GetComponent<TMP_Text>().text = "200";
    }

    public void ChangeQuantity(int quantity)
    {
        _changeQuantityText.SetQuantity(quantity);
        if (quantity == 0)
            _coinPanel.SetActive(true);
    }
}
