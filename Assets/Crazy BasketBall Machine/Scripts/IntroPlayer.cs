using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroPlayer : MonoBehaviour
{
    [SerializeField] VideoPlayer m_VideoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        // Subscribe to the loopPointReached event
        m_VideoPlayer.loopPointReached += OnVideoFinished;

    }


    // Event handler for the loopPointReached event
    void OnVideoFinished(VideoPlayer videoPlayer)
    {
        SceneManager.LoadScene("start");
    }
}
