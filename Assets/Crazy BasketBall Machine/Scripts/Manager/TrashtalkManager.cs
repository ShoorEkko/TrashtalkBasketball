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
    private int m_CurrentPlayerIndex = 0; // Track the current player index

    [SerializeField] private int m_CurrentVideo; // A video to set in the VideoPlayers
    [SerializeField] private List<StageClass> m_StageList;
    [SerializeField] private VideoPlayer[] m_VideoPlayer;
    [SerializeField] private GameMgr m_GameMgr; // Game Manager

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

    public IEnumerator OnStart()
    {
        for (int i = 0; i < m_VideoPlayer.Length; i++)
        {
            m_VideoPlayer[i].gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(0.3f);
        if (!m_GameMgr.isTutorialMode)m_Trashtalk();
    }

    public void OnMiss()
    {
        for (int i = 0; i < m_VideoPlayer.Length; i++)
        {
            if (m_VideoPlayer[i].isPlaying)
                return;
        }

        if (m_Trashtalk != null)
            m_Trashtalk();
    }

    public void OnLastTutorial()
    {
        for (int i = 0; i < m_VideoPlayer.Length; i++)
        {
            m_VideoPlayer[i].gameObject.SetActive(true);
            m_VideoPlayer[0].SetDirectAudioVolume(0, 0); // Disable audio from video player 1
            m_VideoPlayer[i].Play();
        }
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

        if (m_CurrentVideo != m_LastPlayedVideoIndex)
        {
            m_LastPlayedVideoIndex = m_CurrentVideo;

            StartCoroutine(PlayVideoOnAvailablePlayer());
        }
        else
        {
            PlayRandomTrashtalk();
        }
    }

    IEnumerator PlayVideoOnAvailablePlayer()
    {
        // Wait until there is an available VideoPlayer
        while (IsAnyPlayerPlaying())
        {
            yield return null;
        }

        VideoPlayer currentPlayer = m_VideoPlayer[m_CurrentPlayerIndex];

        currentPlayer.gameObject.SetActive(true);
        currentPlayer.clip = m_StageList[m_GameMgr.currentlevel].m_VideoList[m_CurrentVideo];
        currentPlayer.Prepare();

        yield return new WaitUntil(() => currentPlayer.isPrepared);

        currentPlayer.Play();

        yield return new WaitUntil(() => !currentPlayer.isPlaying);

        currentPlayer.Stop();
        currentPlayer.gameObject.SetActive(false);

        // Increment the player index for the next video
        m_CurrentPlayerIndex = (m_CurrentPlayerIndex + 1) % m_VideoPlayer.Length;

        if (m_VideoPlayer[m_CurrentPlayerIndex].isPlaying || IsAnyPlayerPlaying())
        {
            // If any player is still playing, play the next video
            PlayRandomTrashtalk();
        }
    }

    bool IsAnyPlayerPlaying()
    {
        for (int i = 0; i < m_VideoPlayer.Length; i++)
        {
            if (m_VideoPlayer[i].isPlaying)
            {
                return true;
            }
        }
        return false;
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
