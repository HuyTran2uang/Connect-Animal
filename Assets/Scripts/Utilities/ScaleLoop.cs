using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleLoop : MonoBehaviour
{
    Tween tweener;
    [SerializeField] float originalScale, targetScale, time;

    private void OnEnable()
    {
        tweener = transform.DOScale(Vector3.one * targetScale, time).SetLoops(-1 , LoopType.Yoyo);
    }

    private void OnDisable()
    {
        tweener.Kill();
        transform.localScale = Vector3.one * originalScale;
    }
}
