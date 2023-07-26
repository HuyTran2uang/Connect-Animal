using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheSecondTutorialPanel : MonoBehaviour
{
    [SerializeField] List<Transform> _points;
    [SerializeField] List<Image> _lines;
    [SerializeField] List<Transform> _nodes;
    [SerializeField] List<Transform> _stars;
    List<Tween> _tweens = new List<Tween>();
    List<Tweener> _tweeners = new List<Tweener>();

    public void StartTutorial()
    {
        _tweens = new List<Tween>();
        _tweeners = new List<Tweener>();
        _points.ForEach(i => i.localScale = Vector3.zero);
        _lines.ForEach(i => i.fillAmount = 0);
        _lines.ForEach(i => i.color = new Color32(255, 255, 255, 255));
        _nodes.ForEach(i => i.localScale = Vector3.one);
        _stars.ForEach(i => i.localScale = Vector3.zero);
        ShowLine1();
        gameObject.SetActive(true);
    }

    private void ShowLine1()
    {
        var t1 = _points[0].DOScale(Vector3.one * .7f, .3f).SetDelay(.25f);
        _tweeners.Add(t1);
        var t2 = _lines[0].DOFillAmount(1, .5f).OnComplete(ShowLine2);
        _tweeners.Add(t2);
    }

    private void ShowLine2()
    {
        var t = DOVirtual.DelayedCall(.2f, delegate
        {
            var t1 = _points[1].DOScale(Vector3.one * .7f, .3f).SetDelay(.325f);
            _tweeners.Add(t1);
            var t2 = _lines[1].DOFillAmount(1, .75f).OnComplete(ShowLine3);
            _tweeners.Add(t2);
        });
        _tweens.Add(t);
    }

    private void ShowLine3()
    {
        var t = DOVirtual.DelayedCall(.2f, delegate
        {
            var t1 = _points[2].DOScale(Vector3.one * .7f, .3f).SetDelay(.25f);
            _tweeners.Add(t1);
            var t2 = _lines[2].DOFillAmount(1, .5f).OnComplete(ShowStars);
            _tweeners.Add(t2);
        });
        _tweens.Add(t);
    }

    private void ShowStars()
    {
        _stars.ForEach(i => i.DOScale(Vector3.one * .7f, .75f));
        _nodes.ForEach(i => i.DOScale(Vector3.zero, .5f));
        _points.ForEach(i => i.DOScale(Vector3.zero, .3f));
        var t = DOVirtual.DelayedCall(1f, Hide);
        _tweens.Add(t);
    }

    private void Hide()
    {
        _stars.ForEach(i => i.DOScale(Vector3.zero, .75f));
        _lines.ForEach(i => i.DOFade(0, .75f));
        var t = DOVirtual.DelayedCall(1f, StartTutorial);
        _tweens.Add(t);
    }

    public void Done()
    {
        _tweens?.ForEach(i => i.Kill());
        _tweeners?.ForEach(i => i.Kill());
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Done();
    }
}
