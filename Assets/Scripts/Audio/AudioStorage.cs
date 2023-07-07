using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class AudioStorage : ScriptableObject
{
    private static AudioStorage _instance;

    public static AudioStorage Instance
    {
        get
        {
            if (_instance == null)
                _instance = Resources.Load<AudioStorage>("Data/Audio Storage");
            return _instance;
        }
    }

    [SerializeField] private Audio[] _audios;

    public Audio[] Audios => _audios;
}
