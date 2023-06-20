using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePreloader : MonoBehaviour
{
    public static ScenePreloader Instance;
    private AsyncOperation preloadOperation;
    private bool playButtonClicked = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        preloadOperation = SceneManager.LoadSceneAsync("game", LoadSceneMode.Additive);
        preloadOperation.completed += OnPreloadComplete;
        preloadOperation.allowSceneActivation = false;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnPreloadComplete(AsyncOperation operation)
    {
        if (playButtonClicked)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("game"));
            SceneManager.UnloadSceneAsync("start");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.name == "game" && playButtonClicked)
        {
            SceneManager.SetActiveScene(scene);
            SceneManager.UnloadSceneAsync("start");
        }
    }

    public void PlayButtonClicked()
    {
        playButtonClicked = true;

        if (preloadOperation.isDone)
        {
            OnPreloadComplete(preloadOperation);
        }
        else
        {
            preloadOperation.allowSceneActivation = true;
        }
    }
}
