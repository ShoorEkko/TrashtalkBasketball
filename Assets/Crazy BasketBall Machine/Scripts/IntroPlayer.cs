using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class IntroPlayer : MonoBehaviour
{
    [SerializeField] private VideoPlayer m_VideoPlayer;
    [SerializeField] private GameObject m_Disclaimer;
    [SerializeField] private float m_FadeDuration = 1f;

    private void Start()
    {
        // Subscribe to the loopPointReached event
        m_VideoPlayer.loopPointReached += OnVideoFinished;
    }

    private void OnVideoFinished(VideoPlayer videoPlayer)
    {
        StartCoroutine(ShowDisclaimerAndLoadScene());
    }

    private IEnumerator ShowDisclaimerAndLoadScene()
    {
        // Fade in the disclaimer
        m_Disclaimer.SetActive(true);
        m_Disclaimer.GetComponent<CanvasGroup>().alpha = 0f;
        yield return m_Disclaimer.GetComponent<CanvasGroup>().DOFade(1f, m_FadeDuration).WaitForCompletion();

        // Wait for 4 seconds
        yield return new WaitForSeconds(4f);

        // Fade out the disclaimer
        yield return m_Disclaimer.GetComponent<CanvasGroup>().DOFade(0f, m_FadeDuration).WaitForCompletion();
        m_Disclaimer.SetActive(false);

        // Load a different scene
        SceneManager.LoadScene(1);
    }
}
