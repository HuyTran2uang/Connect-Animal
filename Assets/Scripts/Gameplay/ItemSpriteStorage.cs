using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSpriteStorage : ScriptableObject
{
    [SerializeField] private List<Sprite> _sprites = new List<Sprite>();

    public List<Sprite> Sprites => _sprites;
}