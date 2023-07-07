using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Audio
{
    public AudioName name;
    public AudioType type;
    public AudioClip clip;
    public bool loop;
}

public enum AudioType
{
    Sound,
    Music
}

public enum AudioName
{
    Music,
    Click,
}