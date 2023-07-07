using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StarSpawner : MonoBehaviourSingleton<StarSpawner>
{
    [SerializeField] private GameObject _starPrefab;
    [SerializeField] private Transform _target;
    private List<GameObject> _stars = new List<GameObject>();

    public GameObject StarSpawned(Vector3 pos)
    {
        GameObject star = Instantiate(_starPrefab, pos, Quaternion.identity, transform);
        _stars.Add(star);
        return star;
    }

    public void TakeStar()
    {
        if (_stars.Count <= 0) return;
        foreach (var star in _stars)
            StarMoveToTarget(star);
        _stars.Clear();
    }

    private void StarMoveToTarget(GameObject star)
    {
        star.transform.DORotate(new Vector3(0, 0, 180), 1f);
        star.transform.DOMove(_target.position, 1f).OnComplete(delegate
        {
            StarManager.Instance.AddStar(1);
            Destroy(star);
        });
    }
}
