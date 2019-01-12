using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstUIBehaviour : MonoBehaviour
{
	public string firstSceneName;

	public void launchGame()
	{
		SceneManager.LoadScene(firstSceneName);
	}

	public void exitGame()
	{
		Debug.Log("Application quit");
		Application.Quit();
	}
}
