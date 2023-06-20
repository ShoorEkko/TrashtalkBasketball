using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour 
{
	[SerializeField] ScenePreloader scenePreloader;


	public void playgame()
	{
		scenePreloader.PlayButtonClicked();
	}



}
