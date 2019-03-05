using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformBehaviour : MonoBehaviour {

	public Vector2 direction;
	public float speed;

	private Rigidbody2D body;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		body = gameObject.GetComponent<Rigidbody2D>();
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		body.MovePosition((Vector2)transform.position + (direction * speed));
	}

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "movement boundary")
			direction = -direction;
	}
}
