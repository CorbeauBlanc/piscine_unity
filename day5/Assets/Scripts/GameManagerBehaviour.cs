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

	public int maxForce = 1000;

	public GameObject UI;
	public Image loadingBar;

	public bool charging = false;
	public int force = 0;
	public int currentHole = 0;

	private int chargingFactor;
	private CameraBehaviour camBehaviour;
	private ArrowBehaviour arrowBehaviour;
	private Rigidbody ballRigidbody;
	private bool clubMode = false;
	private bool ballThrown = true;


// Start is called before the first frame update
	void Start()
	{
		instance = this;
		camBehaviour = freeCamera.GetComponentInChildren<CameraBehaviour>();
		arrowBehaviour = arrow.GetComponent<ArrowBehaviour>();
		golfBall = Instantiate(golfBall, starts[0].transform.position, Quaternion.identity);
		ballRigidbody = golfBall.GetComponent<Rigidbody>();
		cameraSetBallView();
		chargingFactor = 4 * maxForce / 100;
	}

	public void cameraSetBallView()
	{
		freeCamera.transform.position = golfBall.transform.position;
		camBehaviour.LookToward(flags[currentHole]);
		freeCamera.transform.position = freeCamera.transform.position + 4 * Camera.main.transform.up - 10 * Camera.main.transform.forward;
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
		cameraSetBallView();
		golfBall.transform.position = starts[currentHole].transform.position;
		camBehaviour.Free = false;
	}

	private void setClubMode(bool value)
	{
		if (!value)
		{
			UI.SetActive(false);
			arrow.SetActive(false);
			loadingBar.rectTransform.localScale = new Vector3(0, 1, 1);
			charging = false;
			clubMode = false;
		} else
		{
			UI.SetActive(true);
			arrow.transform.position = golfBall.transform.position - arrow.transform.forward * arrowBehaviour.distance;
			arrow.SetActive(true);
			clubMode = true;
			cameraSetBallView();
		}
	}

	private void throwBall()
	{
		ballRigidbody.AddForce(-arrow.transform.forward * force);
		setClubMode(false);
		force = 0;
		ballThrown = true;
	}

	private void switchCamMode()
	{
		camBehaviour.Free = !camBehaviour.Free;
		setClubMode(!camBehaviour.Free);
		arrow.SetActive(true);
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyUp("e"))
			switchCamMode();

		if (ballThrown && ballRigidbody.velocity.magnitude < .1 && !clubMode)
		{
			camBehaviour.Free = false;
			setClubMode(true);
			ballThrown = false;
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

		if (Input.GetKeyUp("space"))
		{
			if (clubMode)
			{
				if (!charging)
					charging = true;
				else
					throwBall();
			} else if (camBehaviour.Free)
				switchCamMode();
		}
	}

}
