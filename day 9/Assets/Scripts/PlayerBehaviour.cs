using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
	public WeaponBehaviour[] weapons;
	public int currentSelectedWeapon = 0;
	public GameObject impactDust;
	public GameObject impactShower;
	public GameObject impactExplosion;

	private float fwCurrentTime;

	private void selectWeapon(int nb)
	{
		foreach(WeaponBehaviour weapon in weapons)
			weapon.gameObject.SetActive(false);
		weapons[nb].gameObject.SetActive(true);
		currentSelectedWeapon = nb;
	}

	private void Update()
	{
		fwCurrentTime += Time.deltaTime;

		if (Input.GetButton("Fire1") && fwCurrentTime > weapons[currentSelectedWeapon].weaponFireRate)
		{
			weapons[currentSelectedWeapon].shoot();
			fwCurrentTime = 0;
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, weapons[currentSelectedWeapon].weaponFireRange, ~LayerMask.GetMask("Player")))
			{
				Instantiate(impactDust, hit.point, Quaternion.identity);
				if (currentSelectedWeapon == 1)
					Instantiate(impactExplosion, hit.point, Quaternion.identity);
				else
					Instantiate(impactShower, hit.point, Quaternion.identity);
			}
		}

		if (Input.GetKeyDown("1"))
			selectWeapon(0);
		if (Input.GetKeyDown("2"))
			selectWeapon(1);
	}
}
