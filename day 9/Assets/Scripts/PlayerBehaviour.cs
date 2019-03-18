using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
	public static PlayerBehaviour instance { get; private set; }
	public WeaponBehaviour[] weapons;
	public int currentSelectedWeapon = 0;
	public GameObject impactShower;
	public GameObject impactExplosion;
	public GameObject rocket;
	public int life = 20;

	private float fwCurrentTime;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		instance = this;
	}

	private void Update()
	{
		fwCurrentTime += Time.deltaTime;

		if (Input.GetButton("Fire1") && fwCurrentTime > weapons[currentSelectedWeapon].weaponFireRate)
		{
			weapons[currentSelectedWeapon].shoot();
			fwCurrentTime = 0;
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, weapons[currentSelectedWeapon].weaponFireRange, LayerMask.GetMask("Terrain", "AI")))
			{
				Instantiate(weapons[currentSelectedWeapon].impactParticle, hit.point, Quaternion.identity);
				if (weapons[currentSelectedWeapon].tag == "Rocket Launcher")
					Instantiate(rocket, hit.point, Quaternion.identity)
						.GetComponent<RocketBehaviour>().rocketDamage = weapons[currentSelectedWeapon].weaponDamages;
				if (hit.collider.tag == "Ennemi")
					hit.collider.GetComponentInParent<AIBehaviour>().TakeHit(weapons[currentSelectedWeapon].weaponDamages);
			}
		}

		if (Input.GetKeyDown("1"))
			selectWeapon(0);
		if (Input.GetKeyDown("2"))
			selectWeapon(1);
	}

	private void selectWeapon(int nb)
	{
		foreach(WeaponBehaviour weapon in weapons)
			weapon.gameObject.SetActive(false);
		weapons[nb].gameObject.SetActive(true);
		fwCurrentTime = weapons[nb].weaponFireRate;
		currentSelectedWeapon = nb;
	}

	public bool LooseLife()
	{
		life--;
		if (life <= 0)
		{
			Application.Quit();
			Debug.Log("END GAME");
			return true;
		}
		return false;
	}
}
