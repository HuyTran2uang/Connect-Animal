using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUI : MonoBehaviour, IChangeLevelText
{
    [SerializeField] TMP_Text _levelText;

    public void ChangeLevelText(int level)
    {
        _levelText.text = level.ToString();
    }
}
