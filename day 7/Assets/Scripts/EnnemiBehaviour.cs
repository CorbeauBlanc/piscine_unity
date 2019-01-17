using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiBehaviour : MonoBehaviour
{
	public NavMeshAgent agent;
	public GameObject gunSparks;
	public GameObject missileSparks;
	public int gunRange = 50;
	public int missileRange = 100;
	public float gunFireRate = .5F;
	public float missileFireRate = 1;
	public int missileNumber = 5;
	public AudioSource gunSound;
	public AudioSource missileSound;
	public AudioSource explosionSound;
	public GameObject explosionParticles;

	private float gunCurrentTime = 0;
	private float missileCurrentTime = 0;
	private Vector3 lastPlayerPosition;

	private void Update()
	{
		gunCurrentTime += Time.deltaTime;
		missileCurrentTime += Time.deltaTime;

		if (lastPlayerPosition != PlayerBehaviour.instance.gameObject.transform.position)
		{
			lastPlayerPosition = PlayerBehaviour.instance.gameObject.transform.position;
			agent.destination = lastPlayerPosition;
		}
	}

	public void Die()
	{
		explosionSound.Play();
		Instantiate(explosionParticles, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
