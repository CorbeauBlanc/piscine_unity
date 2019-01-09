using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonBlowerBehaviour : MonoBehaviour
{
	public GameObject balloon;
	public float scaleFactor = .5F;
	public float deflateSpeed = .2F;
	public float breathingSpeed = .2F;
	public int maxBreath = 20;
	public int breath = 20;

	private float deflateTimer = 0;
	private float breathTimer = 0;

	void Update()
    {
		deflateTimer += Time.deltaTime;
		breathTimer += Time.deltaTime;
		if (!balloon)
			return;
		if (Input.GetKeyDown("space") && breath > 0)
		{
			balloon.transform.localScale += new Vector3(scaleFactor, scaleFactor, 0);
			breath--;
		}
		else
		{
			if (deflateTimer >= deflateSpeed)
			{
				balloon.transform.localScale -= new Vector3(scaleFactor, scaleFactor, 0);
				deflateTimer = 0;
			}
			if (breathTimer >= breathingSpeed && breath < maxBreath)
			{
				breath++;
				breathTimer = 0;
			}
		}
    }
}
