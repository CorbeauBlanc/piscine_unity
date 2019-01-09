using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownBehaviour : MonoBehaviour
{
	public GameObject spawnPoint;
	public GameObject soldier;

	private float spawnTime = 10;

    // Update is called once per frame
    void Update()
    {
		spawnTime -= Time.deltaTime;
		if (spawnTime <= 0)
		{
			Instantiate(soldier, spawnPoint.transform.position, Quaternion.identity);
			spawnTime = 10;
		}
    }
}
