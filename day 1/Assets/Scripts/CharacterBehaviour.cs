using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{

	public float force;
	public float speed;
	public bool isInExit = false;
	public string characterLayerName;

	private bool active;
	private int index = -1;
	private Rigidbody2D body;
	private BoxCollider2D cdr;
	private Camera mainCam;
	private int initialLayer;

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
			if (body && value)
			{
				body.bodyType = RigidbodyType2D.Dynamic;
				gameObject.layer = initialLayer;
			}
		}
	}

	public bool isGrounded()
	{
		Vector2 pos = new Vector2(transform.position.x, transform.position.y - cdr.bounds.extents.y - .1f);
		return Physics2D.RaycastNonAlloc(pos, Vector2.down, new RaycastHit2D[5], .1f, LayerMask.GetMask(new string[] { "Default", "Scene", characterLayerName })) != 0;
	}

	private void Start()
	{
		body = gameObject.GetComponent<Rigidbody2D>();
		cdr = gameObject.GetComponent<BoxCollider2D>();
		mainCam = Camera.main;
		initialLayer = gameObject.layer;
	}

	private void tryMove(Vector2 direction)
	{
		Vector2 pos = new Vector2(transform.position.x + (direction.x * (cdr.bounds.extents.x + .1f)), transform.position.y);
		RaycastHit2D[] hit = new RaycastHit2D[1];
		if (Physics2D.LinecastNonAlloc(pos, pos + (direction * speed), hit,
										LayerMask.GetMask(new string[] { "Default", "Scene", characterLayerName })) == 0)
			gameObject.transform.Translate(new Vector3(direction.x * speed, 0, 0));
		else
			gameObject.transform.position = new Vector2(hit[0].point.x - (direction.x * (cdr.bounds.extents.x + .01f)),
														gameObject.transform.position.y);
	}

	private void FixedUpdate()
	{
		if (Active && !isInExit)
		{
			if (Input.GetKeyDown("space") && isGrounded())
				body.AddForce(new Vector2(0, force * 10000));

			if (Input.GetKey("left"))
				tryMove(new Vector2(-1, 0));

			if (Input.GetKey("right"))
				tryMove(new Vector2(1, 0));
		} else if (isGrounded() && body.bodyType != RigidbodyType2D.Static)
		{
			body.bodyType = RigidbodyType2D.Static;
			gameObject.layer = 8;
		}
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		if (Active)
			mainCam.transform.position = new Vector3(transform.position.x, transform.position.y, - 10);
	}
}
