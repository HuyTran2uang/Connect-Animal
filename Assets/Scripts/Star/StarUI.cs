using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarUI : MonoBehaviour, IChangeStarText
{
    [SerializeField] TMP_Text _starText;
    [SerializeField] TMP_Text _starTextInLevel;
    [SerializeField] TMP_Text _starTextReceived;


    public void ChangeStarText(int quantity)
    {
        _starText.text = quantity.ToString();
    }

    public void ChangeStarTextInLevel(int level)
    {
        _starTextInLevel.text = level.ToString();
        _starTextReceived.text = level.ToString();
    }
}
