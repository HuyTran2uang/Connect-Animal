using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StarSpawner : MonoBehaviourSingleton<StarSpawner>
{
    [SerializeField] private GameObject _starPrefab;
    [SerializeField] private Transform _target;

    public Transform Target => _target;

    public GameObject StarSpawned(Vector3 pos)
    {
        GameObject star = Instantiate(_starPrefab, pos, Quaternion.identity, transform);
        return star;
    }
}
