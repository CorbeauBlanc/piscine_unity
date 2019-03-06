using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehaviour : MonoBehaviour {

	public GameObject confirmPanel;
	public GameObject pauseMenu;

	public void exitPause()
	{
		gameManager.gm.pause(false);
		pauseMenu.SetActive(false);
	}

	public void exitGame()
	{
		confirmPanel.SetActive(true);
	}

	public void confirmExitGame()
	{
		SceneManager.LoadScene("MainScene");
	}

	public void cancelExitGame()
	{
		confirmPanel.SetActive(false);
	}
}
