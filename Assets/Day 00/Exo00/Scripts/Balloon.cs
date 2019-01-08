using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Balloon : MonoBehaviour
{
	public float		maxScale = 5;
	public GameObject	balloon;

	private float		lifeTime = 0;

    void Update()
    {
		lifeTime += Time.deltaTime;
		if ((transform.localScale.x >= maxScale && transform.localScale.y >= maxScale) ||
			(transform.localScale.x <= 0 && transform.localScale.y <= 0))
		{
			GameObject.Destroy(balloon);
			
			Debug.Log("Fin du jeu. Temp de vie du ballon: " + Mathf.RoundToInt(lifeTime) + "s");
		}
    }
}
