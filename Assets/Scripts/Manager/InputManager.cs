using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameManager.Instance.Play();
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            GameManager.Instance.GoToMenuFromBattle();
        }

        if(Input.GetMouseButtonDown(0))
        {
            ItemSpawner.Instance.DetectDown();
        }

        if(Input.GetMouseButtonUp(0))
        {
            ItemSpawner.Instance.DetectUp();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            BoardManager.Instance.Remap();
        }
    }
}
