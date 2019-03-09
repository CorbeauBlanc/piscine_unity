using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
	public float speed = 70;
	public int distance = 20;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey("a"))
			transform.RotateAround(transform.position + transform.forward * distance, transform.up, -Time.deltaTime * speed);
		else if (Input.GetKey("d"))
			transform.RotateAround(transform.position + transform.forward * distance, transform.up, Time.deltaTime * speed);
	}
}
