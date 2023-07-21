using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Star : MonoBehaviour
{
    private void OnEnable()
    {
        transform.DORotate(new Vector3(0, 0, 180), 1.2f);
        StarManager.Instance.AddStar(1);
        transform.DOMove(StarSpawner.Instance.Target.position, .5f).OnComplete(delegate
        {
            Destroy(gameObject);
        }).SetDelay(.2f);
    }
}
