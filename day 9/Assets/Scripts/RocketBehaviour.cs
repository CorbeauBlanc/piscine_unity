using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
	public float explosionTime;
	public int rocketDamage;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		StartCoroutine(RemoveRocket());
	}

	private IEnumerator RemoveRocket()
	{
		yield return new WaitForSeconds(explosionTime);
		Destroy(gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Ennemi")
			other.gameObject.GetComponentInParent<AIBehaviour>().TakeHit(rocketDamage);
	}
}
