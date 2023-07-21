using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Security.Cryptography;
using UnityEngine.UIElements;

public class BlinkBlink : MonoBehaviour
{
    [SerializeField] GameObject _light;
    float _currentTime, _offsetAngle, _timeRotate = 5, _numberCricleRotate = 10;
    public AnimationCurve _curve;

    public IEnumerator BlinkBlinkLight()
    {
        float _startAngle = _offsetAngle;
        _currentTime = 0;
        float _angleWant = (_numberCricleRotate * 360) + _startAngle;
        while (_currentTime < _timeRotate)
        {
            yield return new WaitForEndOfFrame();
            _currentTime += Time.deltaTime;
            float _angleCurrent = _angleWant * _curve.Evaluate(_currentTime / _timeRotate);
            _light.transform.eulerAngles = new Vector3(0, 0, _angleCurrent - _startAngle);
        }
    }

    private void OnEnable()
    {
        BlinkBlinkLight();
    }
}
