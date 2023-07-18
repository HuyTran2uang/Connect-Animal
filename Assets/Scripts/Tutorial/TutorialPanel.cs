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
    [SerializeField] LevelTutorial _levelTutorial;

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
        _nextButton.gameObject.SetActive(true);
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
        _theFirstTutorialPanel.Done();
        _theSecondTutorialPanel.Done();
        _theThirdTutorialPanel.Done();
    }

    public void OpenLevelTutorial()
    {
        CloseTutorials();
        _nextButton.gameObject.SetActive(false);
        _levelTutorial.StartTutorial();
        gameObject.SetActive(true);
    }

    public void CompletedLevelTutorial()
    {
        gameObject.SetActive(false);
        _levelTutorial.gameObject.SetActive(false);
    }
}
