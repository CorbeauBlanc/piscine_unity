using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
	public float		spawningRate = 1;
	public GameObject	redCube;
	public GameObject	greenCube;
	public GameObject	blueCube;

	private float	spawningTimer = 0;

    void Update()
    {
		int pos;

		spawningTimer += Time.deltaTime;
		if (spawningTimer >= spawningRate)
		{
			pos = (int)Random.Range(1, 4);
			switch (pos)
			{
				case 1:
					GameObject.Instantiate(redCube);
					break;
				case 2:
					GameObject.Instantiate(greenCube);
					break;
				case 3:
					GameObject.Instantiate(blueCube);
					break;
			}
			spawningTimer = 0;
		}
    }
}
