using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIBehaviour : MonoBehaviour {

	public Text energyDisplay;
	public Text healthDisplay;
	public GameObject pauseMenu;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		energyDisplay.text = "Energy Level: " + gameManager.gm.playerEnergy;
		healthDisplay.text = "Health: " + gameManager.gm.playerHp;
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		energyDisplay.text = "Energy Level: " + gameManager.gm.playerEnergy;
		healthDisplay.text = "Health: " + gameManager.gm.playerHp;

		if (Input.GetKeyUp("escape"))
		{
			gameManager.gm.pause(true);
			pauseMenu.SetActive(true);
		}
	}

}
