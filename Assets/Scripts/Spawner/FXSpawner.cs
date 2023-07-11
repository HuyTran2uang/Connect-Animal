using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXSpawner : MonoBehaviourSingleton<FXSpawner>
{
    [SerializeField] GameObject _explodeFX, _lightningFX;

    public GameObject ExplodeFX(Vector3 position)
    {
        GameObject fx = Instantiate(_explodeFX, position, Quaternion.identity, transform);
        return fx;
    }

    public GameObject LightningStrikeFX(Vector3 position)
    {
        GameObject fx = Instantiate(_lightningFX, position, Quaternion.identity, transform);
        return fx;
    }
}
