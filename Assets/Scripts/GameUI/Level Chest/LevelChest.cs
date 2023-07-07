using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LevelChest : MonoBehaviour
{
    [SerializeField] GameObject iconCloseChest, iconOpenChest;
    [SerializeField] Button buttonChest;
    [SerializeField] TMP_Text coinReward;

    Tweener animChest;
    int coinRewardNumber;

    public bool isOpened { get; private set; }

    private void Awake()
    {
        buttonChest.onClick.AddListener(DoOpenChest);
    }

    private void DoOpenChest()
    {
        coinRewardNumber = Random.Range(1, 66);
        iconCloseChest.SetActive(false);
        iconOpenChest.SetActive(true);
        coinReward.text = coinRewardNumber.ToString();
        buttonChest.interactable = false;
        isOpened = true;
    }

    public void ResetChest()
    {
        iconCloseChest.SetActive(true);
        iconOpenChest.SetActive(false);
        buttonChest.interactable = true;
        isOpened = false;
    }

    public void SetDeActive()
    {
        buttonChest.interactable = false;
    }

    public void SetAnimChest()
    {
        iconOpenChest.transform.DOScale(1.3f, 1).SetLoops(-1, LoopType.Yoyo);
    }
}
