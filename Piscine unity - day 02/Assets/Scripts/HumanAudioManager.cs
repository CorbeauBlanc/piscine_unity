using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAudioManager : MonoBehaviour
{
	public static HumanAudioManager instance { get; private set; }
	public AudioSource[] acknowlegeSounds;
	public AudioSource[] annoyedSounds;
	public AudioSource[] deadSounds;
	public AudioSource[] helpSounds;
	public AudioSource[] selectedSounds;

	private void Awake()
	{
		instance = this;
	}
	
	public void playRandomAcknowledge()
	{
		acknowlegeSounds[Random.Range(0, acknowlegeSounds.Length)].Play();
	}

	public void playRandomSelected()
	{
		selectedSounds[Random.Range(0, selectedSounds.Length)].Play();
	}

}
