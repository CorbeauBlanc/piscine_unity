using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	public double inertia = .1;
	public double movement = 0;
	public double maxWinSpeed = .01;
	public GameObject ball;

	private int direction = 1;

    void Update()
    {
		if (movement != 0)
		{
			if (ball.transform.position.y + direction * movement < -4.4 || ball.transform.position.y + direction * movement > 4.4)
				direction = -direction;

			ball.transform.Translate(0, (float)(direction * movement), 0);

			if (direction * movement < inertia && direction * movement > -inertia)
				movement = 0;

			if (movement > 0)
				movement -= inertia;
		}

		if (ball.transform.position.y >= 2.6 && ball.transform.position.y <= 2.9 && movement <= maxWinSpeed)
		{
			movement = 0;
			inertia = 0;
			Debug.Log("Win !!!");
		}

	}
}
