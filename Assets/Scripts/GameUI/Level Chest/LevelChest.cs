using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelChest : MonoBehaviour
{
    [SerializeField] GameObject iconCloseChest, iconOpenChest;
    [SerializeField] Button buttonChest;
    [SerializeField] TMP_Text coinReward;

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
}
