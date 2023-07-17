using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviourSingleton<TutorialManager>, IReadData ,IPrepareGame, IAfterPrepareGame
{
    TutorialPanel _tutorialPanel;
    bool _isOpening;
    int _iTutorial;

    public void LoadData()
    {
        _isOpening = Data.ReadData.LoadData(GlobalKey.TUTORIALOPENING, _isOpening);
    }

    public void Prepare()
    {
        _tutorialPanel = FindObjectOfType<TutorialPanel>(true);
    }

    public void AfterPrepareGame()
    {
        if (_isOpening) return;
        StartTutorial();
    }

    public void StartTutorial()
    {
        _isOpening = false;
        _iTutorial = 0;
        NextTutorial();
    }

    public void NextTutorial()
    {
        switch (_iTutorial)
        {
            case 0:
                _tutorialPanel.gameObject.SetActive(true);
                _tutorialPanel.OpenFirstTutorial();
                break;
            case 1:
                _tutorialPanel.OpenSecondTutorial();
                break;
            case 2:
                _tutorialPanel.OpenThirdTutorial();
                break;
            case 3:
                _isOpening = true;
                _tutorialPanel.gameObject.SetActive(false);
                _tutorialPanel.CloseTutorials();
                break;
        }
        _iTutorial++;
    }
}
