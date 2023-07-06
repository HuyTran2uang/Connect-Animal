using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _iconSR;
    [SerializeField] private int _row, _col, _id;
    [SerializeField] GameObject _border;
#if UNITY_EDITOR
    [SerializeField] TMPro.TMP_Text _text;
#endif
    Tweener _highLight;
    float _originalScale = .7f, _hintScale = .4f, _highLightScale = .8f;

    private void Awake()
    {
        transform.localScale = Vector3.one * _originalScale;
    }

    public void Init(int row, int column, Sprite icon, int id)
    {
        _row = row;
        _col = column;
        _iconSR.sprite = icon;
        _id = id;
#if UNITY_EDITOR
        _text.gameObject.SetActive(true);
        _text.text = $"[{_row},{_col}]-id:{_id}";
#endif
    }

    public void SetCoordinate(int row, int col)
    {
        _row = row;
        _col = col;
#if UNITY_EDITOR
        _text.text = $"[{_row},{_col}]-id:{_id}";
#endif
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
        _highLight = transform.DOScale(Vector3.one * _highLightScale, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    public void UnHighLight()
    {
        _highLight.Kill();
        transform.localScale = Vector3.one * _originalScale;
    }

    public void Hint()
    {
        transform.localScale = Vector3.one * _hintScale;
    }

    public void UnHint()
    {
        transform.localScale = Vector3.one * _originalScale;
    }
}
