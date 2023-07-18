using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NodeUI : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] GameObject _highlightBorder;
    [SerializeField] Image _circle;

    private void Reset()
    {
        _button = GetComponent<Button>();
        _highlightBorder = transform.GetChild(1).gameObject;
        _circle = transform.GetChild(2).GetComponent<Image>();
    }

    private void Awake()
    {
        _button.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            Select();
        });
    }

    private void Select()
    {
        LevelTutorial.Instance.SelectNode(this);
        _highlightBorder.SetActive(true);
    }

    public void Hint()
    {
        _button.interactable = true;
    }

    public void ResetStatus()
    {
        _highlightBorder.SetActive(false);
        _button.interactable = false;
    }

    public void Touch()
    {
        _circle.transform.localScale = Vector3.zero;
        _circle.transform.DOScale(Vector3.one * .63f, .5f).OnComplete(delegate
        {
            _circle.gameObject.SetActive(false);
        });
        _circle.gameObject.SetActive(true);
    }
}
