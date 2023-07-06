using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviourSingleton<ItemSpawner>
{
    [SerializeField] Item _itemPrefab;
    List<Item> _items = new List<Item>();
    Item _highLightItem;

    public Item GetItemSpawned(Vector3 pos)
    {
        Item item = Instantiate(_itemPrefab, pos, Quaternion.identity, transform);
        _items.Add(item);
        return item;
    }

    public void ClearItems()
    {
        if (_items.Count > 0)
            for (int i = _items.Count - 1; i >= 0; i--)
                Destroy(_items[i].gameObject);
    }

    public void DetectDown()
    {
        if (GameManager.Instance.GameState != GameState.OnBattle) return;
        if (GameManager.Instance.BattleState != BattleState.None) return;
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.up);
        _highLightItem = hit.collider?.GetComponent<Item>();
        _highLightItem?.HighLight();
    }

    public void DetectUp()
    {
        if (_highLightItem == null) return;
        if (GameManager.Instance.GameState != GameState.OnBattle) return;
        if (GameManager.Instance.BattleState != BattleState.None) return;
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.up);
        Item item = hit.collider?.GetComponent<Item>();
        item?.Select();
        _highLightItem.UnHighLight();
    }

    public void UnHint() => _items.ForEach(i => i.UnHint());
}
