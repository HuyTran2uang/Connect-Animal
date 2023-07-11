using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSpawner : MonoBehaviourSingleton<SpecialSpawner>
{
    [SerializeField] List<SpecialType> _specialTypes = new List<SpecialType>();

    public SpecialType GetRandomSpecialType()
    {
        return _specialTypes[UnityEngine.Random.Range(0, _specialTypes.Count)];
    }

    public void AddSpecialType(SpecialType specialType)
    {
        _specialTypes.Add(specialType);
    }
}
