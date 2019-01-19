using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
	public Animator weaponAnimator;
	public AudioSource shootingAudio;
	public ParticleSystem shootingParticle;
	public float weaponFireRate;
	public float weaponFireRange;

	public void shoot()
	{
		shootingAudio.Play();
		shootingParticle.Play();
		weaponAnimator.SetTrigger("Shooting");
	}
}
