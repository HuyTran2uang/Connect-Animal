using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class TheFirstTutorialPanel : MonoBehaviour
{
    [SerializeField] GameObject _hightLight1, _hightLight2;
    [SerializeField] Image _line;
    [SerializeField] List<Image> _stars = new List<Image>();
    [SerializeField] GameObject _nodeA, _nodeB;
    [SerializeField] Image _hand;
    Vector3 _originalPositionHand;

    private void Awake()
    {
        _originalPositionHand = _hand.transform.position;
    }

    public void StartTutorial()
    {
        _nodeA.transform.localScale = Vector3.one;
        _nodeB.transform.localScale = Vector3.one;
        _hightLight1.SetActive(false);
        _hightLight2.SetActive(false);
        _hand.DOFade(1, .5f);
        _hand.transform.DOMove(_nodeA.transform.position + new Vector3(.2f, -.5f, 0), 1f).OnComplete(delegate
        {
            _hand.transform.DOScale(new Vector3(.5f, .54f, .54f), .1f).SetLoops(1, LoopType.Yoyo).OnComplete(Touch1);
        });
        gameObject.SetActive(true);
    }

    private void Touch1()
    {
        _hightLight1.SetActive(true);
        _hand.transform.DOMove(_nodeB.transform.position + new Vector3(.2f, -.5f, 0), 1f).OnComplete(delegate
        {
            _hand.transform.DOScale(new Vector3(.5f, .54f, .54f), .1f).SetLoops(1, LoopType.Yoyo).OnComplete(Touch2);
        });
    }

    private void Touch2()
    {
        _hightLight2.SetActive(true);
        _hand.DOFade(0, 1f);
        _hand.transform.DOMove(_originalPositionHand, 1f);
        _nodeA.transform.DOScale(Vector3.zero, .5f);
        _nodeB.transform.DOScale(Vector3.zero, .5f);
        ShowConnenctSuccess();
    }

    private void ShowConnenctSuccess()
    {
        ShowLine(delegate
        {
            HideLineAndStars(StartTutorial);
        });
        ShowStars();
    }

    private void ShowLine(Action onCompleted)
    {
        _line.DOFade(1, .5f).OnComplete(delegate
        {
            onCompleted();
        });
    }

    private void ShowStars()
    {
        _stars.ForEach(i => i.transform.DOScale(Vector3.one, .5f).SetEase(Ease.InQuad));
    }

    public void HideLineAndStars(Action onCompleted)
    {
        DOVirtual.DelayedCall(.5f, delegate
        {
            _line.DOFade(0, .5f).OnComplete(delegate { onCompleted(); });
            _stars.ForEach(i => i.transform.DOScale(Vector3.zero, .5f));
        });
    }
}
