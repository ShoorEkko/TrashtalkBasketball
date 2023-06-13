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

    [SerializeField] private float m_Timer;
    [SerializeField] private List<StageClass> m_StageList;
    [SerializeField] private int m_CurrentVideo; // A video to set in the VideoPlayers
    [SerializeField] VideoPlayer[] m_VideoPlayer;
    [SerializeField] GameMgr m_GameMgr; // Game Manager

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
        for (int i = 0; i < m_VideoPlayer.Length; i++) //This checks if theres a video currently playing
        {
            if (m_Trashtalk != null)
                m_Trashtalk();
        }
    }

    public void OnRingshot()
    {
        for (int i = 0; i < m_VideoPlayer.Length; i++) //This checks if theres a video currently playing
        {
            m_VideoPlayer[i].loopPointReached += OnVideoFinished;
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
        m_CurrentVideo = Random.Range(0, m_StageList[m_GameMgr.currentlevel].m_VideoList.Count); //Randomize to get the CurrentVideo Trashtalk
        for (int i = 0; i < m_VideoPlayer.Length; i++) //Check the number of VideoPlayer
        {
            m_VideoPlayer[i].clip = m_StageList[m_GameMgr.currentlevel].m_VideoList[m_CurrentVideo]; // set the Clips of the videoplayer depending on the current stage of the player
            m_VideoPlayer[i].Play();
        }
    }
}
