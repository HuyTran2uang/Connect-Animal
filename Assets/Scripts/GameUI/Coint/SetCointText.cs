using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetCointText : MonoBehaviour
{
    [SerializeField] TMP_Text cointText;

    public void SetCoint(int coint)
    {
        cointText.text = coint.ToString();
    }

}
