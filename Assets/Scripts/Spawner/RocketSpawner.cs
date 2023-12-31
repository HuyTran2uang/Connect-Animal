using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawner : MonoBehaviourSingleton<RocketSpawner>
{
    [SerializeField] Rocket _rocketPrefab;
    List<Rocket> _rockets = new List<Rocket>();
    Action _onCompleted;

    public void ShootRocket(Vector3 pos1, Vector3 pos2, Action onCompleted)
    {
        Couple randCouple = BoardManager.Instance.GetRandomCouple();
        _onCompleted = onCompleted;
        RocketSpawned(pos1).Target(randCouple.Coord1.x, randCouple.Coord1.y);
        RocketSpawned(pos2).Target(randCouple.Coord2.x, randCouple.Coord2.y);
    }

    public Rocket RocketSpawned(Vector3 position)
    {
        Rocket rocket = Instantiate(_rocketPrefab, position, Quaternion.identity, transform);
        return rocket;
    }

    public void OnExlodeCompleted(Rocket rocket)
    {
        rocket.Explode();
        _rockets.Remove(rocket);
        Destroy(rocket.gameObject);
        if (_rockets.Count > 0) return;
        _onCompleted.Invoke();
        GameManager.Instance.ResumeGame();
    }
}
