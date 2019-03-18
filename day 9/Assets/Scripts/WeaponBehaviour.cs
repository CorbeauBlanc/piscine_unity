using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
	public Animator weaponAnimator;
	public AudioSource shootingAudio;
	public GameObject impactParticle;
	public float weaponFireRate;
	public float weaponFireRange;
	public int weaponDamages;

	public void shoot()
	{
		shootingAudio.Play();
		weaponAnimator.SetTrigger("Shooting");
	}
}
