using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour {

	public GameObject[] zombies;
	public int maxZombies;
	public int nbInstantiatedZombies = 0;
	public float spawnRate;

	public bool isSpawning = true;

	private IEnumerator SpawnZombie()
	{
		while (isSpawning)
		{
			if (nbInstantiatedZombies < maxZombies)
			{
				Instantiate(zombies[Random.Range(0, zombies.Length)], transform.position, Quaternion.identity)
					.GetComponent<ZombieBehaviour>().sourceSpawner = this;
				nbInstantiatedZombies++;
			}
			yield return new WaitForSeconds(spawnRate);
		}
	}

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		StartCoroutine(SpawnZombie());
	}
}
