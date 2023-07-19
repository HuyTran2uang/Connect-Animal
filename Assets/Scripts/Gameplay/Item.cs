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
    [SerializeField] Transform _light;
    float _originalScale = .7f, _hintScale = .5f, _highLightScale = .8f;
    bool _isHint;
    List<Tweener> _tweeners = new List<Tweener>();

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
        var check = false;
        var t = transform.DOScale(Vector3.one * _hintScale, .5f).SetLoops(-1, LoopType.Yoyo).OnStepComplete(delegate
        {
            if (!check)
            {
                _light.DOScale(Vector3.one * .3f, .2f).SetLoops(2, LoopType.Yoyo).SetAutoKill();
                transform.DOShakeRotation(.1f, new Vector3(0, 0, 40), 10, 30).SetLoops(2, LoopType.Yoyo).SetAutoKill();
            }
            check = !check;
        });
        _tweeners.Add(t);
    }

    public void UnHint()
    {
        _isHint = false;
        _light.localScale = Vector3.one * .1f;
        _tweeners.ForEach(i => i.Kill());
        transform.localScale = Vector3.one * _originalScale;
    }
}

public enum SpecialType
{
    Rocket,
    Bomb,
    Lightning,
}
