using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Loading : MonoBehaviourSingletonPersistent<Loading>
{
    protected override void Awake()
    {
        base.Awake();
        ApplovinManager.Instance.Init();
        StartCoroutine(LoadAllData());
    } 

    private IEnumerator LoadAllData()
    {
        var readers = FindObjectsOfType<MonoBehaviour>().OfType<IReadData>();
        foreach (var reader in readers)
            reader.LoadData();
        yield return SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive);
        yield return LoadAllPrepare();
    }

    private IEnumerator LoadAllPrepare()
    {
        var prepareGames = FindObjectsOfType<MonoBehaviour>().OfType<IPrepareGame>();
        foreach (var prepareGame in prepareGames)
            prepareGame.Prepare();
        yield return SceneManager.UnloadSceneAsync("Loading");
        yield return LoadAllAfterPrepare();
    }

    private IEnumerator LoadAllAfterPrepare()
    {
        var afterPrepareGames = FindObjectsOfType<MonoBehaviour>().OfType<IAfterPrepareGame>();
        foreach (var after in afterPrepareGames)
            after.AfterPrepareGame();
        yield return null;
        ApplovinManager.Instance.ShowBanner();
    }
}
