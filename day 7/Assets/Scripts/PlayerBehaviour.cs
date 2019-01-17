using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
	public int speed;
	public GameObject body;
	public Rigidbody tankRigidBody;

	private void FixedUpdate()
	{
		float axis;

		if ((axis = Input.GetAxis("Vertical")) > 0 || axis < 0)
			tankRigidBody.AddForce(-axis * speed * body.transform.up);
		if ((axis = Input.GetAxis("Horizontal")) > 0 || axis < 0)
			body.transform.localEulerAngles += Vector3.forward * axis;
	}
}
