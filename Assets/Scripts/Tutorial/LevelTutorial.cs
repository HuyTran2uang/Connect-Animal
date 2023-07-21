using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class LevelTutorial : MonoBehaviourSingleton<LevelTutorial>
{
    [SerializeField] List<GameObject> _lines = new List<GameObject>();
    [SerializeField] List<GameObject> _stars = new List<GameObject>();
    [SerializeField] List<NodeUI> _nodes = new List<NodeUI>();
    [SerializeField] Image _hand;
    NodeUI _nodeA, _nodeB, _hintNodeA, _hintNodeB;
    int _iCouple;
    List<Tweener> _tweeners = new List<Tweener>();
    List<Tween> _tweens = new List<Tween>();

    public void StartTutorial()
    {
        _lines.ForEach(i => i.SetActive(false));
        _stars.ForEach(i => i.SetActive(false));
        _nodes.ForEach(i => i.gameObject.SetActive(true));
        _nodes.ForEach(i => i.ResetStatus());
        HintCouple();
        gameObject.SetActive(true);
    }

    public void SelectNode(NodeUI node)
    {
        if (_nodeA == null)
            _nodeA = node;
        if(_nodeA != null && node != _nodeA)
        {
            _nodeB = node;
            _lines[_iCouple].SetActive(true);
            _stars[_iCouple].SetActive(true);
            _nodeA.gameObject.SetActive(false);
            _nodeB.gameObject.SetActive(false);
            _nodeA = null;
            _nodeB = null;
            _lines[_iCouple].SetActive(false);
            _stars[_iCouple].SetActive(false);
            _iCouple++;
            if (_iCouple < 6)
                HintCouple();
            else
            {
                TutorialManager.Instance.PassLevelTutorial();
                GameManager.Instance.Play();
            }
        }
    }

    public void HintCouple()
    {
        switch (_iCouple)
        {
            case 0:
                _hintNodeA = _nodes[5];
                _hintNodeB = _nodes[6];
                break;
            case 1:
                _hintNodeA = _nodes[4];
                _hintNodeB = _nodes[10];
                break;
            case 2:
                _hintNodeA = _nodes[1];
                _hintNodeB = _nodes[8];
                break;
            case 3:
                _hintNodeA = _nodes[0];
                _hintNodeB = _nodes[2];
                break;
            case 4:
                _hintNodeA = _nodes[7];
                _hintNodeB = _nodes[9];
                break;
            case 5:
                _hintNodeA = _nodes[3];
                _hintNodeB = _nodes[11];
                break;
        }
        _hintNodeA.Hint();
        _hintNodeB.Hint();
        var t = DOVirtual.DelayedCall(.15f, StartHint);
        _tweens.Add(t);
    }

    private void StartHint()
    {
        _tweeners.ForEach(i => i.Kill());
        _tweens.ForEach(i => i.Kill());
        _tweeners = new List<Tweener>();
        _tweens = new List<Tween>();
        _hand.color = new Color(255, 255, 255, 0);
        _hand.transform.position = _hintNodeA.transform.position + new Vector3(0.2f, -0.5f, 0);
        var t = _hand.DOFade(1, .5f).OnComplete(delegate
        {
            MoveToTarget(_hintNodeB.transform.position + new Vector3(0.5f, -0.5f, 0));
        });
        _tweeners.Add(t);
    }

    bool isB = false;

    private void MoveToTarget(Vector3 targetPos)
    {
        isB = false;
        var t = _hand.transform.DOMove(targetPos, 1f).SetLoops(-1, LoopType.Yoyo).OnStepComplete(delegate
        {
            isB = !isB;
            var t1 = _hand.transform.DOShakeScale(.1f, .1f).OnComplete(delegate
            {
                if (!isB) _hintNodeA.Touch();
                else _hintNodeB.Touch();
            });
            _tweeners.Add(t1);
        });
        _tweeners.Add(t);
    }
}
