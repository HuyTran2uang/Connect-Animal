using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelText : MonoBehaviour, ILevelText
{
    [SerializeField] TMP_Text _levelText;

    private void Reset()
    {
        _levelText = GetComponent<TMP_Text>();
    }

    public void SetLevelText(int level)
    {
        _levelText.text = level.ToString();
    }
}
