using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{

	public float speed = 1;

	private CharacterController controller;

	private void Start()
	{
		controller = gameObject.GetComponentInParent<CharacterController>();
	}

	// Update is called once per frame
	void Update()
    {
		if (Input.GetKey("up"))
			controller.Move(transform.forward * speed * Time.deltaTime);
		if (Input.GetKey("down"))
			controller.Move(-transform.forward * speed * Time.deltaTime);
		if (Input.GetKey("left"))
			controller.Move(-transform.right * speed * Time.deltaTime);
		if (Input.GetKey("right"))
			controller.Move(transform.right * speed * Time.deltaTime);
		if (Input.GetKey("home"))
			controller.Move(transform.up * speed * Time.deltaTime);
		if (Input.GetKey("end"))
			controller.Move(-transform.up * speed * Time.deltaTime);
	}
}
