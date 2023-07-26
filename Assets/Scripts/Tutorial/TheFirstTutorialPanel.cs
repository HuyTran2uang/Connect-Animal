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
    List<Tweener> _tweeners = new List<Tweener>();
    List<Tween> _tweens = new List<Tween>();

    private void Awake()
    {
        _originalPositionHand = _hand.transform.position;
    }

    public void StartTutorial()
    {
        _tweeners = new List<Tweener>();
        _nodeA.transform.localScale = Vector3.one;
        _nodeB.transform.localScale = Vector3.one;
        _hightLight1.SetActive(false);
        _hightLight2.SetActive(false);
        _hand.DOFade(1, .5f);
        var tweener1 = _hand.transform.DOMove(_nodeA.transform.position + new Vector3(.2f, -.5f, 0), 1f).OnComplete(delegate
        {
            var tweener2 = _hand.transform.DOScale(new Vector3(.5f, .54f, .54f), .1f).SetLoops(1, LoopType.Yoyo).OnComplete(Touch1);
            _tweeners.Add(tweener2);
        });
        _tweeners.Add(tweener1);
        gameObject.SetActive(true);
    }

    private void Touch1()
    {
        _hightLight1.SetActive(true);
        var tweeners3 = _hand.transform.DOMove(_nodeB.transform.position + new Vector3(.2f, -.5f, 0), 1f).OnComplete(delegate
        {
            var tweener4 = _hand.transform.DOScale(new Vector3(.5f, .54f, .54f), .1f).SetLoops(1, LoopType.Yoyo).OnComplete(Touch2);
            _tweeners.Add(tweener4);
        });
        _tweeners.Add(tweeners3);
    }

    private void Touch2()
    {
        _hightLight2.SetActive(true);
        var tweener5 = _hand.DOFade(0, 1f);
        _tweeners.Add(tweener5);
        var tweener6 =_hand.transform.DOMove(_originalPositionHand, 1f);
        _tweeners.Add(tweener6);
        var tweener7 = _nodeA.transform.DOScale(Vector3.zero, .5f);
        _tweeners.Add(tweener7);
        var tweener8 = _nodeB.transform.DOScale(Vector3.zero, .5f);
        _tweeners.Add(tweener8);
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
        var tweener9 = _line.DOFade(1, .5f).OnComplete(delegate
        {
            onCompleted();
        });
        _tweeners.Add(tweener9);
    }

    private void ShowStars()
    {
        _stars.ForEach(i => i.transform.DOScale(Vector3.one, .5f).SetEase(Ease.InQuad));
    }

    public void HideLineAndStars(Action onCompleted)
    {
        var tween1 = DOVirtual.DelayedCall(.5f, delegate
        {
            _line.DOFade(0, .5f).OnComplete(delegate { onCompleted(); });
            _stars.ForEach(i => i.transform.DOScale(Vector3.zero, .5f));
        });
        _tweens.Add(tween1);
    }

    public void Done()
    {
        _tweeners?.ForEach(i => i.Kill());
        _tweens?.ForEach(i => i.Kill());
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Done();
    }
}
