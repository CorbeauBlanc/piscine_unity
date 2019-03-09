using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class CameraBehaviour : MonoBehaviour
{

	public float speed = 1;
	public GameObject cameraRig;
	public GameObject cameraPivot;
	public CharacterController controller;

	private bool free = false;
	private FreeLookCam cameraScript;

	public bool Free
	{
		get
		{
			return free;
		}
		set
		{
			free = value;
			cameraScript.free = value;
		}
	}

	public void LookToward(GameObject obj)
	{
		cameraRig.transform.LookAt(obj.transform);
		cameraPivot.transform.LookAt(obj.transform);
	}

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		cameraScript = cameraRig.GetComponent<FreeLookCam>();
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
