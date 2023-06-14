using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCheck : MonoBehaviour
{
    [SerializeField] private float currentTimer;
    [SerializeField] private float m_Timer;
    [SerializeField] private bool m_hasTrashTalked = false;
    // Start is called before the first frame update

    void Start() // This script is attached to Basketball Prefab
    {
        Destroy(gameObject, 15);
        currentTimer = m_Timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
        }
    }

        private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag != "Goal")
        {
            if(currentTimer < 0 && m_hasTrashTalked == false)
            {
                TrashtalkManager.Instance.OnMiss();
                m_hasTrashTalked = true;
            }
            
        }
        else
        {
            m_hasTrashTalked = true;
        }
    }
}
