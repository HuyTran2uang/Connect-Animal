using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.Play();
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            GameManager.Instance.Play();
        }
    }
}
