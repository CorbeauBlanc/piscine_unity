using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedButtonsBehaviour : MonoBehaviour {

	public void playNormalSpeed()
	{
		gameManager.gm.changeSpeed(1);
	}

	public void playFastSpeed()
	{
		gameManager.gm.changeSpeed(2);
	}
}
