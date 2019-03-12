using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour {

	public Animator animator;
	public AudioSource openSound;

	public void OpenDoor()
	{
		animator.SetTrigger("Open Door");
		openSound.Play();
	}
}
