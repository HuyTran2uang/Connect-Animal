using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _iconSR;
    [SerializeField] protected int _row, _col, _id;
    [SerializeField] GameObject _border;
    Tweener _highLight, _hint;
    float _originalScale = .7f, _hintScale = .4f, _highLightScale = .8f;
    bool _isHint;

    private void Awake()
    {
        transform.localScale = Vector3.one * _originalScale;
    }

    public void Init(int row, int column, Sprite icon, int id)
    {
        _row = row;
        _col = column;
        _id = id;
        _iconSR.sprite = icon;
    }

    public void SetCoordinate(int row, int col)
    {
        _row = row;
        _col = col;
    }

    public void Select()
    {
        _border.SetActive(true);
        BoardManager.Instance.SelectNode(_row, _col);
    }

    public void UnSelect()
    {
        _border.SetActive(false);
    }

    public void HighLight()
    {
        transform.localScale = Vector3.one * _highLightScale;
    }

    public void UnHighLight()
    {
        transform.localScale = Vector3.one * _originalScale;
    }

    public void Hint()
    {
        if (_isHint) return;
        _isHint = true;
        _hint = transform.DOScale(Vector3.one * _hintScale, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    public void UnHint()
    {
        _isHint = false;
        _hint.Kill();
        transform.localScale = Vector3.one * _originalScale;
    }
}

public enum SpecialType
{
    Rocket,
    Bomb,
    Lightning,
}
