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

    [SerializeField] private List<StageClass> m_StageList;
    [SerializeField] private int m_CurrentVideo; // A video to set in the VideoPlayers
    [SerializeField] VideoPlayer[] m_VideoPlayer;
    [SerializeField] GameMgr m_GameMgr; // Game Manager
    // Start is called before the first frame update
    void Start()
    {
       PlayRandomTrashtalk();
    }

    public void OnStart()
    {

    }

    public void OnMiss()
    {
        if (m_Trashtalk != null)
            m_Trashtalk += PlayRandomTrashtalk;
    }

    public void OnRingshot()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           //insert trashtalk here to test  
        }
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
