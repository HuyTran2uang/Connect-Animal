using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour
{
    [SerializeField] Button _nextButton;
    [SerializeField] TheFirstTutorialPanel _theFirstTutorialPanel;
    [SerializeField] TheSecondTutorialPanel _theSecondTutorialPanel;
    [SerializeField] TheThirdTutorialPanel _theThirdTutorialPanel;

    private void Awake()
    {
        _nextButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlaySoundClickButton();
            TutorialManager.Instance.NextTutorial();
        });
    }

    public void OpenFirstTutorial()
    {
        _theFirstTutorialPanel.StartTutorial();
    }

    public void OpenSecondTutorial()
    {
        CloseTutorials();
        _theSecondTutorialPanel.StartTutorial();
    }

    public void OpenThirdTutorial()
    {
        CloseTutorials();
        _theThirdTutorialPanel.StartTutorial();
    }

    public void CloseTutorials()
    {
        _theFirstTutorialPanel.gameObject.SetActive(false);
        _theSecondTutorialPanel.gameObject.SetActive(false);
        _theThirdTutorialPanel.gameObject.SetActive(false);
    }
}
