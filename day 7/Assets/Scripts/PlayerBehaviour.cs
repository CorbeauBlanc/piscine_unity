using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
	public static PlayerBehaviour instance { get; private set; }

	public float speed = .1F;
	public float turretSpeed = 1;
	public float bodyRotationSpeed = 2;
	public GameObject body;
	public GameObject turret;
	public GameObject gunSparks;
	public GameObject missileSparks;
	public GameObject explosionParticles;
	public CharacterController controller;
	public bool boostActive = true;
	public float maxBoost = 10;
	public int gunRange = 25;
	public int missileRange = 50;
	public float gunFireRate = .5F;
	public float missileFireRate = 1;
	public int missileNumber = 5;
	public AudioSource gunSound;
	public AudioSource missileSound;
	public GameObject explosionSound;

	private float boost = 1;
	private float gunCurrentTime = 0;
	private float missileCurrentTime = 0;
	private int health = 100;

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
			turret.transform.localEulerAngles += turret.transform.forward * axis * turretSpeed;

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
					if (hit.collider.gameObject.tag == "Ennemi")
						hit.collider.gameObject.GetComponentInParent<EnnemiBehaviour>().TakeDamage(5, gameObject);
					Instantiate(gunSparks, hit.point, Quaternion.identity);
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
						hit.collider.gameObject.GetComponentInParent<EnnemiBehaviour>().TakeDamage(10, gameObject);
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
			controller.Move(-transform.up * 10);
	}

	public bool TakeDamage(int damage, GameObject source)
	{
		if (health <= 0)
			return true;
		health -= damage;
		if (health <= 0)
		{
			Instantiate(explosionSound, transform.position, Quaternion.identity);
			Instantiate(explosionParticles, transform.position, Quaternion.identity);
			Camera.main.transform.SetParent(null);
			Destroy(gameObject);
			return true;
		}

		return false;
	}
}
