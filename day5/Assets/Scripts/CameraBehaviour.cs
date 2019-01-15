using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class CameraBehaviour : MonoBehaviour
{

	public float speed = 1;

	public bool Free
	{
		get => free;
		set
		{
			free = value;
			cameraRig.free = value;
		}
	}

	public FreeLookCam cameraRig;

	private bool free = false;
	private CharacterController controller;

	private void Start()
	{
		controller = gameObject.GetComponentInParent<CharacterController>();
	}

	// Update is called once per frame
	void Update()
    {
		if (!free)
			return;

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
