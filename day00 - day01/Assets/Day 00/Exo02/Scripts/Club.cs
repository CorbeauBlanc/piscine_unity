using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : MonoBehaviour
{
	public Ball ball;
	public int strength;
	public double givenMovement = 0;

	private int score = -20;

    void Update()
    {
		if (Input.GetKey("space"))
			givenMovement += strength * ball.inertia;
		else if (ball.movement == 0 && givenMovement != 0)
		{
			if (!ball.won)
				score += 5;
			if (score < 0)
				Debug.Log("Score: " + score);
			else
				Debug.Log("Perdu ! Score: " + score);
			ball.movement = givenMovement;
			givenMovement = 0;
		}
    }
}
