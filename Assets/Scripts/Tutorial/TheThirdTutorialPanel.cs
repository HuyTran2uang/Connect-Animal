using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheThirdTutorialPanel : MonoBehaviour
{
    [SerializeField] List<Image> _lines = new List<Image>();
    [SerializeField] List<Transform> _points = new List<Transform>();
    [SerializeField] Transform _board;
    List<Tween> _tweens = new List<Tween>();
    List<Tweener> _tweeners = new List<Tweener>();
         
    public void StartTutorial()
    {
        _tweens = new List<Tween>();
        _tweeners = new List<Tweener>();
        _lines.ForEach(i => i.fillAmount = 0);
        _points.ForEach(i => i.localScale = Vector3.zero);
        ShowLine1();
        gameObject.SetActive(true);
    }

    private void ShowLine1()
    {
        var tweener1 = _points[0].DOScale(Vector3.one * .8f, .5f).SetDelay(.25f);
        _tweeners.Add(tweener1);
        var tweener2 = _lines[0].DOFillAmount(1, .5f).OnComplete(ShowLine2);
        _tweens.Add(tweener2);
    }

    private void ShowLine2()
    {
        var tweener1 = _points[1].DOScale(Vector3.one * .8f, .5f).SetDelay(.25f);
        _tweens.Add(tweener1);
        var tweener2 = _lines[1].DOFillAmount(1, .5f).OnComplete(ShowLine3);
        _tweens.Add(tweener2);
    }

    private void ShowLine3()
    {
        var t1 = _points[2].DOScale(Vector3.one * .8f, .5f).SetDelay(.25f);
        _tweens.Add(t1);
        var t2 = _lines[2].DOFillAmount(1, .5f).OnComplete(ShowLine4);
        _tweens.Add(t2);
    }

    private void ShowLine4()
    {
        var t1 = _points[3].DOScale(Vector3.one * .8f, .5f).SetDelay(.25f);
        _tweens.Add(t1);
        var t2 = _lines[3].DOFillAmount(1, .5f).OnComplete(ShakeBoard);
        _tweens.Add(t2);
    }

    private void ShakeBoard()
    {
        var tweener = _board.DOShakeScale(.2f).SetLoops(2, LoopType.Yoyo);
        _tweeners.Add(tweener);
        var t = DOVirtual.DelayedCall(.5f, StartTutorial);
        _tweens.Add(t);
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
