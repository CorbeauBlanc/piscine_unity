using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerBehaviour : MonoBehaviour
{
	public static GameManagerBehaviour instance { get; private set; }

	public GameObject golfBall;
	public GameObject arrow;
	public GameObject freeCamera;
	public List<GameObject> flags;
	public List<GameObject> starts;

	public int maxForce = 200000;

	public GameObject UI;
	public Image loadingBar;

	public bool charging = false;
	public int force = 0;
	public int currentHole = 0;

	private int chargingFactor = 4000;
	private CameraBehaviour camBehaviour;
	private Rigidbody ballRigidbody;
	private bool clubMode = false;

	public void cameraSetBallView()
	{
		freeCamera.transform.position = golfBall.transform.position;
		freeCamera.transform.LookAt(flags[currentHole].transform);
		freeCamera.transform.position = freeCamera.transform.position + 20 * Camera.main.transform.up - 80 * Camera.main.transform.forward;
	}

	public void resetGame()
	{
		if (currentHole >= starts.Count)
		{
			Application.Quit();
			return;
		}

		ballRigidbody.velocity = Vector3.zero;
		setClubMode(false);
		golfBall.transform.position = starts[currentHole].transform.position;
	}

	private void setClubMode(bool value)
	{
		if (!value)
		{
			UI.SetActive(false);
			arrow.SetActive(false);
			charging = false;
			clubMode = false;
		} else
		{
			UI.SetActive(true);
			arrow.transform.position = golfBall.transform.position - arrow.transform.forward * 20;
			arrow.SetActive(true);
			clubMode = true;
		}
	}

	private void throwBall()
	{
		ballRigidbody.AddForce(-arrow.transform.forward * force);
		setClubMode(false);
		force = 0;
	}

	// Start is called before the first frame update
	void Start()
	{
		instance = this;
		camBehaviour = freeCamera.GetComponentInChildren<CameraBehaviour>();
		golfBall = Instantiate(golfBall, starts[0].transform.position, Quaternion.identity);
		ballRigidbody = golfBall.GetComponent<Rigidbody>();
		cameraSetBallView();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyUp("e"))
		{
			camBehaviour.Free = !camBehaviour.Free;
			if (!camBehaviour.Free)
				cameraSetBallView();
		}

		if (ballRigidbody.velocity.magnitude < 1 && !clubMode)
		{
			ballRigidbody.velocity = Vector3.zero;
			setClubMode(true);
			cameraSetBallView();
		}

		if (charging)
		{
			force += chargingFactor;
			if (force >= maxForce)
			{
				force = maxForce;
				chargingFactor = -chargingFactor;
			}
			if (force <= 0)
			{
				force = 0;
				chargingFactor = -chargingFactor;
			}

			loadingBar.rectTransform.localScale = new Vector3((float)force / (float)maxForce, 1, 1);
		}

		if (Input.GetKeyUp("space") && clubMode)
		{
			if (!charging)
				charging = true;
			else
				throwBall();
		}
	}
}
