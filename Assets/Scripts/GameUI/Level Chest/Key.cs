using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] GameObject key;
    [SerializeField] GameObject unKey;

    public void UnKey()
    {
        key.SetActive(false);
        unKey.SetActive(true);
    }

    public void ActiveKey()
    {
        key.SetActive(true);
        unKey.SetActive(false);
    }
}
