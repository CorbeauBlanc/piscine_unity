using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{

	public float force;
	public float speed;

	private bool active;
	private int index = -1;
	private Rigidbody2D body;
	private BoxCollider2D cdr;
	private Camera mainCam;

	public int Index
	{
		get
		{
			return index;
		}
		set
		{
			index = value;
		}
	}
	public bool Active
	{
		get
		{
			return active;
		}
		set
		{
			active = value;
			if (body)
				body.bodyType = value ? RigidbodyType2D.Dynamic : RigidbodyType2D.Static;
		}
	}

	private void Start()
	{
		body = gameObject.GetComponent<Rigidbody2D>();
		cdr = gameObject.GetComponent<BoxCollider2D>();
		mainCam = Camera.main;
	}

	private void tryMove(Vector2 direction)
	{
		Vector2 pos = new Vector2(transform.position.x + (direction.x * (cdr.bounds.extents.x + .1f)), transform.position.y);
		RaycastHit2D[] hit = new RaycastHit2D[1];
		if (Physics2D.LinecastNonAlloc(pos, pos + (direction * speed), hit,
										LayerMask.GetMask(new string[] { "Default", "Walls", "Characters" })) == 0)
			gameObject.transform.Translate(new Vector3(direction.x * speed, 0, 0));
		else if (hit[0].transform.position.x - pos.x > 0.2 || hit[0].transform.position.x - pos.x < -0.2)
			body.MovePosition(new Vector2(transform.position.x + (direction.x * speed), transform.position.y));
	}

	private bool isGrounded()
	{
		Vector2 pos = new Vector2(transform.position.x, transform.position.y - cdr.bounds.extents.y - .1f);
		return Physics2D.RaycastNonAlloc(pos, Vector2.down, new RaycastHit2D[5], .2f, LayerMask.GetMask(new string[] { "Default", "Scene", "Characters" })) != 0;
	}

	// Update is called once per frame
	private void FixedUpdate()
	{
		if (Active)
		{
			if (Input.GetKeyDown("up") && isGrounded())
				body.AddForce(new Vector2(0, force * 10000));

			if (Input.GetKey("left"))
				tryMove(new Vector2(-1, 0));

			if (Input.GetKey("right"))
				tryMove(new Vector2(1, 0));

			mainCam.transform.position = new Vector3(transform.position.x, transform.position.y, - 10);
		}
	}
}
