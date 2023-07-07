using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    [SerializeField] Button _bombButton, _hintButton, _remapButton;

    private void Awake()
    {
        _bombButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            BombManager.Instance.Throw();
        });

        _hintButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            HintManager.Instance.Hint();
        });

        _remapButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            MapManager.Instance.Remap();
        });
    }
}
