using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePreloader : MonoBehaviour
{
    private AsyncOperation preloadOperation;
    private bool playButtonClicked = false;

    private void Start()
    {
        // Preload the Game scene
        preloadOperation = SceneManager.LoadSceneAsync("game", LoadSceneMode.Additive);
        preloadOperation.completed += OnPreloadComplete;
        preloadOperation.allowSceneActivation = false;
    }

    private void OnPreloadComplete(AsyncOperation operation)
    {
        // Check if the Play button was clicked
        if (playButtonClicked)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("game"));
            SceneManager.UnloadSceneAsync("start");
        }
    }

    public void PlayButtonClicked()
    {
        // Set the flag to indicate the Play button was clicked
        playButtonClicked = true;

        // If the preload is already completed, trigger the OnPreloadComplete method directly
        if (preloadOperation.isDone)
        {
            OnPreloadComplete(preloadOperation);
        }
        else
        {
            // Allow the preload operation to proceed
            preloadOperation.allowSceneActivation = true;
        }
    }
}
