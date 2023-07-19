using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;
using Unity.VisualScripting;

public class SpinWheelPopup : MonoBehaviour
{
    [SerializeField] GameObject _spinWheel;
    [SerializeField] Button _freeSpin, _freeSpinAd, _backButton;

    int _numberOfGift = 9, _countReward;
    float _timeRotate = 5;
    float _numberCricleRotate = 8;
    float _angleOfGift, _currentTime, _indexGiftRandom, _offsetAngle;

    public AnimationCurve _curve;
    private void Awake()
    {
        _angleOfGift = 360 / _numberOfGift;

        _freeSpin.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            StartCoroutine(RotateWheel());
            _freeSpin.interactable = false;
            _countReward += 1;
        });

        _freeSpinAd.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            StartCoroutine(RotateWheel());
            _freeSpinAd.interactable = false;
            _countReward += 1;
        });

        _backButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            gameObject.SetActive(false);
        });
    }

    IEnumerator RotateWheel()
    {
        float _startAngle = _offsetAngle;
        _currentTime = 0;
        _indexGiftRandom = UnityEngine.Random.Range(1, _numberOfGift);
        float _angleWant = (_numberCricleRotate * 360) + _angleOfGift * _indexGiftRandom + _startAngle;
        while (_currentTime < _timeRotate)
        {
            yield return new WaitForEndOfFrame();
            _currentTime += Time.deltaTime;
            float _angleCurrent = _angleWant * _curve.Evaluate(_currentTime / _timeRotate);
            _spinWheel.transform.eulerAngles = new Vector3(0, 0, _angleCurrent - _startAngle);
        }
        yield return new WaitForEndOfFrame();
        switch(_indexGiftRandom){
            case 1:
                Debug.Log("+1 remap");
                RemapManager.Instance.AddRemapTimes(1);
                break;
            case 2:
                Debug.Log("+150 coint");
                CurrencyManager.Instance.AddCoint(150);
                break;
            case 3:
                Debug.Log("+2 hint");
                HintManager.Instance.AddHintTimes(2);
                break;
            case 4:
                Debug.Log("+2 bomb");
                BombManager.Instance.AddThrowTimes(2);
                break;
            case 5:
                Debug.Log("+2 remap");
                RemapManager.Instance.AddRemapTimes(2);
                break;
            case 6:
                Debug.Log("+300 coint");
                CurrencyManager.Instance.AddCoint(300);
                break;
            case 7:
                Debug.Log("+1 hint");
                HintManager.Instance.AddHintTimes(1);
                break;
            case 8:
                Debug.Log("+1 bomb");
                BombManager.Instance.AddThrowTimes(1);
                break;
        }
        if (_countReward == 1)
        {
            _freeSpin.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _spinWheel.transform.eulerAngles = Vector3.zero;
        _countReward = 0;
    }
}