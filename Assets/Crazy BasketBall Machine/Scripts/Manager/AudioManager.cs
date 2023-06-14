using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] m_AudioClips;
    private List<int> m_PlayedClips = new List<int>();

    void Start()
    {
        PlayRandomMusic();
    }

    void PlayRandomMusic()
    {
        if (m_PlayedClips.Count == m_AudioClips.Length)
        {
            // All clips have been played, so reset the played clips list
            m_PlayedClips.Clear();
        }

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, m_AudioClips.Length);
        } while (m_PlayedClips.Contains(randomIndex));

        m_PlayedClips.Add(randomIndex);

        audioSource.clip = m_AudioClips[randomIndex];
        audioSource.Play();

        // Invoke the method again when the current clip finishes playing
        Invoke("PlayRandomMusic", m_AudioClips[randomIndex].length);
    }
}

