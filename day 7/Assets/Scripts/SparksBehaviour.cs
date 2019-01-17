using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparksBehaviour : MonoBehaviour
{
	public ParticleSystem particles;

	private void Update()
	{
		if (!particles.IsAlive())
			Destroy(gameObject);
	}
}
