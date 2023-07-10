using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BombUI : MonoBehaviourSingleton<BombUI>
{
    [SerializeField] ChangeQuantityText _changeQuantityText;
    [SerializeField] GameObject _coinPanel;

    private void Awake()
    {
        _coinPanel.transform.Find("Quantity Text").GetComponent<TMP_Text>().text = "150";
    }

    public void ChangeQuantity(int quantity)
    {
        _changeQuantityText.SetQuantity(quantity);
        _coinPanel.SetActive(quantity == 0);
    }
}
