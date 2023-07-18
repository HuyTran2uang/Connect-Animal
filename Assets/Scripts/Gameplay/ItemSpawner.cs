using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSpawner : MonoBehaviourSingleton<ItemSpawner>
{
    [SerializeField] Item _itemPrefab;
    [SerializeField] SpecialItem _specialItemPrefab;
    [SerializeField] List<GameObject> _specialIconPrefabs; 
    [SerializeField] List<Item> _items = new List<Item>();
    Item _highLightItem;

    public Item GetItemSpawned(Vector3 pos)
    {
        Item item = Instantiate(_itemPrefab, pos, Quaternion.identity, transform);
        _items.Add(item);
        return item;
    }

    public SpecialItem GetSpecialItemSpawned(Vector3 pos, SpecialType specialType)
    {
        SpecialItem item = Instantiate(_specialItemPrefab, pos, Quaternion.identity, transform);
        Instantiate(_specialIconPrefabs[(int)specialType], item.transform);
        _items.Add(item);
        return item;
    }


    public void ClearItems()
    {
        if (_items.Count > 0)
            for (int i = _items.Count - 1; i >= 0; i--)
                Destroy(_items[i].gameObject);
        _items.Clear();
    }

    public void DetectDown()
    {
#if UNITY_EDITOR
        if (EventSystem.current.IsPointerOverGameObject()) return;
#else
        foreach (var touch in Input.touches)
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId)) return;
#endif
        if (GameManager.Instance.GameState != GameState.OnBattle) return;
        if (GameManager.Instance.BattleState != BattleState.None) return;
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward);
        _highLightItem = hit.collider?.GetComponent<Item>();
        _highLightItem?.HighLight();
    }

    public void DetectUp()
    {
        if (_highLightItem == null) return;
        if (GameManager.Instance.GameState != GameState.OnBattle) return;
        if (GameManager.Instance.BattleState != BattleState.None) return;
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward);
        Item item = hit.collider?.GetComponent<Item>();
        if (item == _highLightItem)
            _highLightItem.Select();
        _highLightItem.UnHighLight();
    }

    public void UnHint() => _items.ForEach(i => i.UnHint());
    public void UnSelectAll() => _items.ForEach(i => i.UnSelect());
}
