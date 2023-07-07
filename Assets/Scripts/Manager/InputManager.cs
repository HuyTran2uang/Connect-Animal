using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ItemSpawner.Instance.DetectDown();
        }

        if(Input.GetMouseButtonUp(0))
        {
            ItemSpawner.Instance.DetectUp();
        }
    }
}
