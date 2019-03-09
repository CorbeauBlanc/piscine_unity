using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleBehaviour : MonoBehaviour
{
	public int holeNumber;

	private void OnTriggerEnter(Collider other)
	{
		if (holeNumber == GameManagerBehaviour.instance.currentHole)
			GameManagerBehaviour.instance.currentHole++;
		GameManagerBehaviour.instance.resetGame();
	}

}
