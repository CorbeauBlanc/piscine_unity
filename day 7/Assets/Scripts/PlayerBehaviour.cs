using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
	public static PlayerBehaviour instance { get; private set; }

	public float speed = .1F;
	public float turretSpeed = .1F;
	public float bodyRotationSpeed = 2;
	public GameObject body;
	public GameObject turret;
	public GameObject gunSparks;
	public GameObject missileSparks;
	public CharacterController controller;
	public bool boostActive = true;
	public float maxBoost = 10;
	public int gunRange = 50;
	public int missileRange = 100;
	public float gunFireRate = .5F;
	public float missileFireRate = 1;
	public int missileNumber = 5;
	public AudioSource gunSound;
	public AudioSource missileSound;

	private float boost = 1;
	private float gunCurrentTime = 0;
	private float missileCurrentTime = 0;

	private void Start()
	{
		instance = this;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void reloadBoost() => boostActive = true;

	private void Update()
	{
		float axis;

		if ((axis = Input.GetAxis("Vertical")) > 0 || axis < 0)
			controller.Move(-axis * speed * body.transform.up * (boost > 1 ? boost -= .1F : boost = 1));
		if ((axis = Input.GetAxis("Horizontal")) > 0 || axis < 0)
			body.transform.localEulerAngles += Vector3.forward * axis * bodyRotationSpeed;
		if ((axis = Input.GetAxis("Mouse X")) > 0 || axis < 0)
			turret.transform.localEulerAngles += Vector3.up * axis;

		gunCurrentTime += Time.deltaTime;
		missileCurrentTime += Time.deltaTime;

		if (Input.GetKeyDown("left shift") && boostActive)
		{
			boost = maxBoost;
			boostActive = false;
			Invoke("reloadBoost", 10);
		}
		if (Input.GetAxis("Fire1") > 0)
		{
			if (gunCurrentTime > gunFireRate)
			{
				RaycastHit hit;
				if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, gunRange))
				{
					Instantiate(gunSparks, hit.point, Quaternion.identity);
					gunSound.Stop();
					gunSound.Play();
					gunCurrentTime = 0;
				}
			}
		}

		if (Input.GetAxis("Fire2") > 0 && missileNumber > 0)
		{
			if (missileCurrentTime > missileFireRate)
			{
				RaycastHit hit;
				if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, missileRange))
				{
					if (hit.collider.gameObject.tag == "Ennemi")
						hit.collider.gameObject.GetComponentInParent<EnnemiBehaviour>().Die();
					else
						Instantiate(missileSparks, hit.point, Quaternion.identity);
					missileSound.Play();
					missileNumber--;
					missileCurrentTime = 0;
				}
			}
		}
	}

	private void FixedUpdate()
	{
		if (!controller.isGrounded)
		{
			RaycastHit hit;
			if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 256, LayerMask.GetMask("Terrain")))
				Debug.LogError("Pb raycast");
			controller.Move(-transform.up * hit.distance);
		}
	}
}
