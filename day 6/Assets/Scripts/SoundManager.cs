using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance { get; private set; }

	public AudioSource normal;
	public AudioSource panic;
	public AudioSource alarm;

	private void Start() => instance = this;

	public void playMusicNormal()
	{
		if (!normal.isPlaying)
		{
			panic.Stop();
			normal.Play();
		}
	}

	public void playMusicPanic()
	{
		if (!panic.isPlaying)
		{
			normal.Stop();
			panic.Play();
		}
	}

	public void playAlarm()
	{
		if (!alarm.isPlaying)
			alarm.Play();
	}
}
