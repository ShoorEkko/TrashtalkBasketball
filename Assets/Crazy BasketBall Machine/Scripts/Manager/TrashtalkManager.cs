using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TrashtalkManager : MonoBehaviour
{
    [System.Serializable]
    public class StageClass
    {
        public string name = "Stage 1";
        public List<VideoClip> m_VideoList;
    }

    delegate void Trashtalk();
    Trashtalk m_Trashtalk;

    private static TrashtalkManager instance;

    private int m_LastPlayedVideoIndex = -1;

    [SerializeField] private int m_CurrentVideo; // A video to set in the VideoPlayers
    [SerializeField] private List<StageClass> m_StageList;
    [SerializeField] VideoPlayer[] m_VideoPlayer;
    [SerializeField] GameMgr m_GameMgr; // Game Manager

    private List<int> m_PlayedVideos = new List<int>(); // Track the played videos
    private List<int> m_RemainingVideos = new List<int>(); // Track the remaining videos

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        m_Trashtalk += PlayRandomTrashtalk;
    }

    public static TrashtalkManager Instance
    {
        get { return instance; }
    }

    public void OnStart()
    {
        m_Trashtalk();
    }

    public void OnMiss() // Setting a timer if ball went in or not during shooting
    {
        for (int i = 0; i < m_VideoPlayer.Length; i++) //This checks if there's a video currently playing
        {
            if (m_Trashtalk != null)
                m_Trashtalk();
        }
    }

    public void OnRingshot()
    {
        for (int i = 0; i < m_VideoPlayer.Length; i++) //This checks if there's a video currently playing
        {
            if (m_Trashtalk != null)
                m_Trashtalk();
        }
    }

    void OnVideoFinished(VideoPlayer videoPlayer)
    {
        // Do something when the video finishes playing
        if (m_Trashtalk != null)
            m_Trashtalk();
    }

    void PlayRandomTrashtalk()
{
    if (m_RemainingVideos.Count == 0)
    {
        ResetPlayedVideos();
        ShuffleRemainingVideos();
    }

    int randomIndex = Random.Range(0, m_RemainingVideos.Count);
    m_CurrentVideo = m_RemainingVideos[randomIndex];
    m_RemainingVideos.RemoveAt(randomIndex);
    m_PlayedVideos.Add(m_CurrentVideo);

    // Check if the current video was the last played video
    if (m_CurrentVideo != m_LastPlayedVideoIndex)
    {
        m_LastPlayedVideoIndex = m_CurrentVideo;

        for (int i = 0; i < m_VideoPlayer.Length; i++)
        {
            if (m_VideoPlayer[i].isPlaying)
                return;

            m_VideoPlayer[i].clip = m_StageList[m_GameMgr.currentlevel].m_VideoList[m_CurrentVideo];
            m_VideoPlayer[i].Play();
        }
    }
    else
    {
        PlayRandomTrashtalk(); // Call the method again to select a different video
    }
}

    void ResetPlayedVideos()
    {
        m_PlayedVideos.Clear();
        for (int i = 0; i < m_StageList[m_GameMgr.currentlevel].m_VideoList.Count; i++)
        {
            m_RemainingVideos.Add(i);
        }
    }

    void ShuffleRemainingVideos()
    {
        for (int i = 0; i < m_RemainingVideos.Count; i++)
        {
            int temp = m_RemainingVideos[i];
            int randomIndex = Random.Range(i, m_RemainingVideos.Count);
            m_RemainingVideos[i] = m_RemainingVideos[randomIndex];
            m_RemainingVideos[randomIndex] = temp;
        }
    }
}
