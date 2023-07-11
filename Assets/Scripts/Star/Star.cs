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
        transform.DOMove(StarSpawner.Instance.Target.position, 1f).OnComplete(delegate
        {
            StarManager.Instance.AddStar(1);
            Destroy(gameObject);
        }).SetDelay(.3f);
    }
}
