using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SwitchBehaviour : MonoBehaviour {

	public DoorBehaviour door;
	public GameObject lockedPannel;
	public GameObject unlockedPannel;
	public AudioSource deniedSound;

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			if (other.GetComponent<FirstPersonController>().hasKey)
			{
				lockedPannel.SetActive(false);
				unlockedPannel.SetActive(true);
				door.OpenDoor();
			} else
				deniedSound.Play();
		}
	}
}
