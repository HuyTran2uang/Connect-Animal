using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CointMove : MonoBehaviour
{
    Tween tween;
    [SerializeField] GameObject cointPrefab;
    [SerializeField] float time = 1, timeDelay = 0.5f;

    [SerializeField] GameObject targetcoint;

    private void CointMovePos(GameObject coint, float index)
    {
        tween = coint.transform.DOMove(targetcoint.transform.position, time).SetDelay(timeDelay + index/10).OnComplete(delegate
        {
            coint.SetActive(false);
        });
    }
    public void ActiveCoint()
    {
        for (int i = 0; i < 5; i++)
        {
            float posX = UnityEngine.Random.Range(-.2f, .2f);
            GameObject cointIntance = Instantiate(cointPrefab, transform);
            cointIntance.transform.position += new Vector3(posX, 0, 0);
            CointMovePos(cointIntance, i);
        }
    }
}
