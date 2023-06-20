using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePreloader : MonoBehaviour
{
    public static ScenePreloader Instance;
    private AsyncOperation preloadOperation;
    private bool playButtonClicked = false;

    [SerializeField]
    private GameObject loadingUI; // Reference to the loading UI object

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

    private IEnumerator StartSceneLoadingDelay()
    {
        // Enable the loading UI object
        loadingUI.SetActive(true);

        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // Load the "start" scene
        SceneManager.LoadScene("start");

        if (preloadOperation.isDone)
        {
            OnPreloadComplete(preloadOperation);
        }
        else
        {
            preloadOperation.allowSceneActivation = true;
        }

        // Disable the loading UI object
        loadingUI.SetActive(false);
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
        if (scene.name == "start" && !playButtonClicked)
        {
            StartCoroutine(StartSceneLoadingDelay());
        }
        else if (scene.name == "game" && playButtonClicked)
        {
            SceneManager.SetActiveScene(scene);
            SceneManager.UnloadSceneAsync("start");

            // Disable the loading UI object
            loadingUI.SetActive(false);
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
