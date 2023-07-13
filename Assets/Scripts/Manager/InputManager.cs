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

        if (Input.GetKeyDown(KeyCode.W))
        {
            BoardManager.Instance.Up();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            BoardManager.Instance.Down();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            BoardManager.Instance.Left();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            BoardManager.Instance.Right();
        }
    }
}
