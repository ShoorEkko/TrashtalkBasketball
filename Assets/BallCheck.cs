using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCheck : MonoBehaviour
{
    [SerializeField] float m_Timer;
    [SerializeField] bool m_hasTrashTalked = false;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 15);
    }

    // Update is called once per frame
    void Update()
    {
        while (m_Timer > 0f)
        {
            // Decrease timer value
            m_Timer -= Time.deltaTime;

        }
    }

        private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Goal")
        {
            if(m_Timer < 0 && m_hasTrashTalked == false)
            {
                TrashtalkManager.Instance.OnMiss();
                m_hasTrashTalked = true;
            }
            
        }
            gameObject.tag = "Untagged";
    }
}
