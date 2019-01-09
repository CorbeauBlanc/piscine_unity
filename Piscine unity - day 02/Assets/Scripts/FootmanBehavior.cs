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

	private CharacterMovement currentDirection = CharacterMovement.STOP;
	private Vector2 newPosition;
	private Vector2 currentVector;
	private Rigidbody2D body;
	private Camera mainCam;


	public void MoveTo(Vector2 coords)
	{
		newPosition = coords;
		currentVector = (coords - (Vector2)gameObject.transform.position).normalized;
		currentDirection = GetCurrentDirection(currentVector);
		gameObject.GetComponent<Animator>().SetTrigger(GetCurrentTrigger(currentDirection));
		HumanAudioManager.instance.playRandomAcknowledge();
	}

	private CharacterMovement GetCurrentDirection(Vector2 currentVector)
	{
		float angle = Vector2.SignedAngle(new Vector2(1, 0), currentVector);
		if ((angle >= -22.5 && angle <= 0) || (angle >= 0 && angle <= 22.5))
			return CharacterMovement.RIGHT;
		if (angle >= 22.5 && angle <= 67.5)
			return CharacterMovement.UPRIGHT;
		if (angle >= 67.5 && angle <= 112.5)
			return CharacterMovement.UP;
		if (angle >= 112.5 && angle <= 157.5)
			return CharacterMovement.UPLEFT;
		if ((angle >= 157.5 && angle <= 180) || (angle <= -157.5 && angle >= -180))
			return CharacterMovement.LEFT;
		if (angle <= -112.5 && angle >= -157.5)
			return CharacterMovement.DOWNLEFT;
		if (angle <= -67.5 && angle >= -112.5)
			return CharacterMovement.DOWN;
		if (angle <= -22.5 && angle >= -67.5)
			return CharacterMovement.DOWNRIGHT;
		return CharacterMovement.STOP;
	}

	private string GetCurrentTrigger(CharacterMovement currentDirection)
	{
		return (new string[] { "Stop",
							"Walk up",
							"Walk up left",
							"Walk left",
							"Walk down left",
							"Walk down",
							"Walk down right",
							"Walk right",
							"Walk up right" })[(int)currentDirection];
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
		if (currentDirection != CharacterMovement.STOP)
		{
			if (Vector2.Distance(gameObject.transform.position, newPosition) < minDistance)
			{
				gameObject.GetComponent<Animator>().SetTrigger("Stop");
				currentDirection = CharacterMovement.STOP;
			}
			else
				body.MovePosition((Vector2)gameObject.transform.position + (currentVector * speed * Time.deltaTime));
		}
    }
}
