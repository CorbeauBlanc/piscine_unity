using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarBehaviour : MonoBehaviour
{
	// Update is called once per frame
	void Update()
	{
		if (transform.localScale.x > 1)
		{
			SoundManager.instance.playMusicPanic();
			//SoundManager.instance.playAlarm();
		}
		else
			SoundManager.instance.playMusicNormal();
	}
}
