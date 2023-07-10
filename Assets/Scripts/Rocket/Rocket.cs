using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Bomb
{
    protected override void MoveCompleted()
    {
        RocketSpawner.Instance.OnExlodeCompleted(this);
    }
}
