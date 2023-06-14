using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class RetryButtonEvents : MonoBehaviour 
{

	public void playgame()
	{
	
	
		AnalyticsManager.Instance.OnReplayGame();
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}



}
