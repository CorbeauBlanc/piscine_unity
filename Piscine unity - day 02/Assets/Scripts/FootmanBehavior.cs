using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterMovement
{
	STOP,
	UP,
	UPLEFT,
	LEFT,
	DOWNLEFT,
	DOWN,
	DOWNRIGHT,
	RIGHT,
	UPRIGHT
}

public class FootmanBehavior : MonoBehaviour
{
	public float minDistance;
	public float speed; 

	private CharacterMovement currentMove = CharacterMovement.STOP;
	private Vector2 newPosition;
	private Vector2 currentVector;
	private Rigidbody2D body;
	private Camera mainCam;

	public void MoveTo(Vector2 coords)
	{
		newPosition = coords;
		currentVector = (coords - (Vector2)gameObject.transform.position).normalized;
		currentMove = CharacterMovement.UP;
	}

    // Start is called before the first frame update
    void Start()
    {
		body = gameObject.GetComponent<Rigidbody2D>();
		mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
		mainCam.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);
		if (Input.GetButtonDown("Fire1"))
			MoveTo(mainCam.ScreenToWorldPoint(Input.mousePosition));
		if (currentMove != CharacterMovement.STOP)
		{
			if (Vector2.Distance(gameObject.transform.position, newPosition) < minDistance)
				currentMove = CharacterMovement.STOP;
			else
				body.MovePosition((Vector2)gameObject.transform.position + (currentVector * speed * Time.deltaTime));
		}
    }
}
