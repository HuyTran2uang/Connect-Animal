using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Unity.VisualScripting;

public class LevelChest : MonoBehaviour
{
    [SerializeField] GameObject iconCloseChest, iconOpenChest;
    [SerializeField] Button buttonChest;
    [SerializeField] TMP_Text coinReward;
    [SerializeField] CointMove _coints;

    Tweener animChest;
    int coinRewardNumber;

    public bool isOpened { get; private set; }
    public Button ButtonChest => buttonChest;

    public void Awake()
    {
        buttonChest.onClick.AddListener(DoOpenChest);
    }

    public void DoOpenChest()
    {
        if (LevelChestPanel.Instance.CountKey <= 0) return;
        coinRewardNumber = Random.Range(1, 200);
        iconCloseChest.SetActive(false);
        iconOpenChest.SetActive(true);
        coinReward.text = coinRewardNumber.ToString();
        _coints.ActiveCoint();
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
}
