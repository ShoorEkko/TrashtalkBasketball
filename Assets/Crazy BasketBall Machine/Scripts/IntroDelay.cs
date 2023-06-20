using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroDelay : MonoBehaviour
{
    [SerializeField] GameObject m_Loading;

    private void Start()
    {
        StartCoroutine(StartSceneLoadingDelay());
    }

    private IEnumerator StartSceneLoadingDelay()
    {
        // Wait for 3 seconds
        m_Loading.SetActive(true);
        yield return new WaitForSeconds(3f);
        m_Loading.SetActive(false);
    }
}
