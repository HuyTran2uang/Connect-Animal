using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomePanel : MonoBehaviour
{
    [SerializeField] Button _playButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            GameManager.Instance.Play();
            UIManager.Instance.OnBattle();
        });
    }
}
