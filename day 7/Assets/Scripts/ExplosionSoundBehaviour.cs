using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSoundBehaviour : MonoBehaviour {
	public AudioSource sound;

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		if (!sound.isPlaying)
			Destroy(gameObject);
	}
}
