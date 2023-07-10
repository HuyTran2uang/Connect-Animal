using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXSpawner : MonoBehaviourSingleton<FXSpawner>
{
    [SerializeField] GameObject _explodeFX;
    List<GameObject> _listFX = new List<GameObject>();

    public GameObject ExplodeFX(Vector3 position)
    {
        GameObject fx = Instantiate(_explodeFX, position, Quaternion.identity, transform);
        _listFX.Add(fx);
        return fx;
    }
}
