using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviourSingleton<TutorialManager>, IReadData ,IPrepareGame, IAfterPrepareGame
{
    TutorialPanel _tutorialPanel;
    bool _isOpening, _isPassedLevelTutorial;
    int _iTutorial;

    public bool IsPassedLevelTutorial => _isPassedLevelTutorial;

    public void LoadData()
    {
        _isOpening = Data.ReadData.LoadData(GlobalKey.TUTORIAL_OPENING, true);
        _isPassedLevelTutorial = Data.ReadData.LoadData(GlobalKey.LEVEL_TUTORIAL_PASSED, false);
    }

    public void Prepare()
    {
        _tutorialPanel = FindObjectOfType<TutorialPanel>(true);
    }

    public void AfterPrepareGame()
    {
        if (!_isOpening) return;
        StartTutorial();
    }

    public void StartTutorial()
    {
        GameManager.Instance.OpenTutorial();
        _isOpening = true;
        Data.WriteData.Save(GlobalKey.TUTORIAL_OPENING, _isOpening);
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
                _isOpening = false;
                Data.WriteData.Save(GlobalKey.TUTORIAL_OPENING, _isOpening);
                _tutorialPanel.gameObject.SetActive(false);
                _tutorialPanel.CloseTutorials();
                GameManager.Instance.CloseTutorial();
                break;
        }
        _iTutorial++;
    }

    public void PassLevelTutorial()
    {
        _tutorialPanel.CompletedLevelTutorial();
        _isPassedLevelTutorial = true;
        Data.WriteData.Save(GlobalKey.LEVEL_TUTORIAL_PASSED, _isPassedLevelTutorial);
        GameManager.Instance.CloseTutorial();
    }

    public void OpenLevelTutorial()
    {
        _tutorialPanel.OpenLevelTutorial();
    }
}
