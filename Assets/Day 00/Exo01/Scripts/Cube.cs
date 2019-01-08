using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cube : MonoBehaviour
{
	public float		movementSpeed = .03F;
	public GameObject	cube;
	public int			line;

	private float	movementTimer = 0;
	private float	cubeSpeed;

	void Start()
	{
		cubeSpeed = UnityEngine.Random.Range(.05F, .2F);
	}

    void Update()
    {
		movementTimer += Time.deltaTime;

		if ((Input.GetKeyDown("a") && line == 1) ||
			(Input.GetKeyDown("u") && line == 2) ||
			(Input.GetKeyDown("i") && line == 3))
		{
			if (cube.transform.position.y > -3.9 && cube.transform.position.y < -2.7)
			{
				GameObject.Destroy(cube);
				Debug.Log("Précision: " + Math.Abs(3.2 + cube.transform.position.y));
				return;
			}
		}

		if (movementTimer >= movementSpeed)
		{
			cube.transform.Translate(0, -cubeSpeed, 0);
			movementTimer = 0;
		}
		if (cube.transform.position.y <= -4.2)
			GameObject.Destroy(cube);
    }
}
