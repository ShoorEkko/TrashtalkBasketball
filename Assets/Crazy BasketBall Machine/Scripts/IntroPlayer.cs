using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroPlayer : MonoBehaviour
{
    [SerializeField] VideoPlayer m_VideoPlayer;
    [SerializeField] AudioSource m_AudioSource;

    private bool hasVideoFinished;

    // Start is called before the first frame update
    void Start()
    {
        // Subscribe to the loopPointReached event
        m_VideoPlayer.loopPointReached += OnVideoFinished;
        hasVideoFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasVideoFinished)
        {
            m_AudioSource.gameObject.SetActive(true);
            m_VideoPlayer.gameObject.SetActive(false);
        }
    }

    // Event handler for the loopPointReached event
    void OnVideoFinished(VideoPlayer videoPlayer)
    {
        hasVideoFinished = true;
    }
}
