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

    [SerializeField] private List<StageClass> m_StageList;
    [SerializeField] private int m_CurrentStage; // find a way to know the stage in GameMgr
    [SerializeField] private int m_CurrentVideo; // A video to set in the VideoPlayers
    [SerializeField] VideoPlayer[] m_VideoPlayer;
    [SerializeField] GameMgr m_GameMgr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Remove this if code is done
        {
            PlayRandomTrashtalk();
        }
    }

    public void PlayRandomTrashtalk()
    {
        m_CurrentStage = m_GameMgr.currentlevel;
        m_CurrentVideo = Random.Range(0, m_StageList[m_CurrentStage].m_VideoList.Count); //Randomize to get the trashtalk
        for (int i = 0; i < m_VideoPlayer.Length; i++) //Check the number of VideoPlayer
        {
            m_VideoPlayer[i].clip = m_StageList[m_CurrentStage].m_VideoList[m_CurrentVideo]; // set the Clips of the videoplayer depending on the current stage of the player
            m_VideoPlayer[i].Play();
        }
    }
}
