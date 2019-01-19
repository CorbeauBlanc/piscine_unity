using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
	private void explode(Collider other)
	{
		other.gameObject.GetComponent<AIBehaviour>().takeHit(5);
		Destroy(gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "ai")
			explode(other);
	}
}
