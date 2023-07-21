using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class DailyPopup : MonoBehaviour
{
    [SerializeField] Button _buttonCollect;
    [SerializeField] DayReward _dayRewardPrefab;
    [SerializeField] Transform _container;


    private void Awake()
    {
        _buttonCollect.onClick.AddListener(delegate
        {
            CurrencyManager.Instance.AddCoint(DailyManager.Instance.CointDayReward[DailyManager.Instance.DayCount]);
            AudioManager.Instance.PlaySoundClickButton();
            DailyManager.Instance.SelectReward();
            gameObject.SetActive(false);
        });
    }

    public void SetListItemUI(int[] daycoint, int dayCount)
    {
        for (int i = 0; i < daycoint.Length; i++)
        {
            DayReward dayReward = Instantiate(_dayRewardPrefab, _container);
            if (i <  dayCount)
            {
                dayReward.Init(DailyState.IsOpened, daycoint[i], i+1);
            }
            else if (i == dayCount)
            {
                dayReward.Init(DailyState.IsSelect, daycoint[i], i + 1);
            }
            else
            {
                dayReward.Init(DailyState.IsClose, daycoint[i], i + 1);
            }

        }
    }
}
