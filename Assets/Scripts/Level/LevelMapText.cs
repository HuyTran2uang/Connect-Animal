using UnityEngine;
using TMPro;

public class LevelMapText : MonoBehaviour, ILevelMapText
{
    [SerializeField] TMP_Text _levelText;

    private void Reset()
    {
        _levelText = GetComponent<TMP_Text>();
    }

    public void SetLevelText(int level)
    {
        _levelText.text = $"Level {level}";
    }
}
