using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiBehaviour : MonoBehaviour
{
	public NavMeshAgent agent;
	public SphereCollider weaponCollider;
	public GameObject gunSparks;
	public GameObject missileSparks;
	public GameObject explosionParticles;
	public GameObject turret;
	public int gunRange = 25;
	public int missileRange = 50;
	public float gunFireRate = .5F;
	public float missileFireRate = 1;
	public int missileNumber = 5;
	public float turretSpeed = .1F;

	public int aIPrecision = 10;
	public AudioSource gunSound;
	public AudioSource missileSound;
	public GameObject explosionSound;

	private float gunCurrentTime = 0;
	private float missileCurrentTime = 0;
	private GameObject lastTarget = null;
	private Vector3 lastTargetPosition = Vector3.zero;
	private int health = 100;

	private Vector3 tmp = Vector3.zero;

	private void Update()
	{
		float angle;

		gunCurrentTime += Time.deltaTime;
		missileCurrentTime += Time.deltaTime;

		if (lastTarget)
		{
			tmp.Set(lastTarget.transform.position.x - turret.transform.position.x,
					lastTarget.transform.position.y - turret.transform.position.y,
					lastTarget.transform.position.z - turret.transform.position.z);

			if ((angle = Vector3.SignedAngle(-turret.transform.up, tmp, turret.transform.forward)) > .01F || angle < -.01F)
				turret.transform.localEulerAngles += turret.transform.forward * angle * turretSpeed * Time.deltaTime;

			RaycastHit hit;
			if (Physics.Raycast(turret.transform.position - turret.transform.up * 1.5F, -turret.transform.up, out hit, missileNumber > 0 ? missileRange : gunRange))
			{
				if (hit.collider.tag != "Terrain" || Vector3.Distance(hit.point, lastTargetPosition) < aIPrecision)
				{
					agent.isStopped = true;
					if (tmp.magnitude < gunRange)
						ShootGun(hit);
					else if (missileNumber > 0)
						ShootMissile(hit);
					else
						agent.isStopped = false;
				}
			}
			else if (Physics.Linecast(turret.transform.position, lastTarget.transform.position, LayerMask.GetMask("Terrain")))
				lastTarget = null;
			else if (lastTargetPosition != lastTarget.transform.position)
			{
				lastTargetPosition = lastTarget.transform.transform.position;
				agent.destination = lastTargetPosition;
				agent.isStopped = false;
			}
			else
				agent.isStopped = false;
		}
	}

	private void ShootMissile(RaycastHit hit)
	{
		if (missileCurrentTime > missileFireRate)
		{
			if (hit.collider.gameObject.tag == "Ennemi")
			{
				if (hit.collider.gameObject.GetComponentInParent<EnnemiBehaviour>().TakeDamage(10, gameObject))
					lastTarget = null;
			}
			else if (hit.collider.gameObject.tag == "Player")
			{
				if (hit.collider.gameObject.GetComponentInParent<PlayerBehaviour>().TakeDamage(10, gameObject))
					lastTarget = null;
			}
			Instantiate(missileSparks, hit.point, Quaternion.identity);
			missileSound.Play();
			if ((missileNumber--) <= 0)
				weaponCollider.radius = gunRange;
			missileCurrentTime = 0;
		}
	}

	private void ShootGun(RaycastHit hit)
	{
		if (gunCurrentTime > gunFireRate)
		{
			if (hit.collider.gameObject.tag == "Ennemi")
			{
				if (hit.collider.gameObject.GetComponentInParent<EnnemiBehaviour>().TakeDamage(5, gameObject))
					lastTarget = null;
			}
			else if (hit.collider.gameObject.tag == "Player")
			{
				if (hit.collider.gameObject.GetComponentInParent<PlayerBehaviour>().TakeDamage(5, gameObject))
					lastTarget = null;
			}
			Instantiate(gunSparks, hit.point, Quaternion.identity);
			gunSound.Play();
			gunCurrentTime = 0;
		}
	}

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Terrain" && !lastTarget)
			SetLastTarget(other.gameObject);
	}

	/// <summary>
	/// OnTriggerStay is called once per frame for every Collider other
	/// that is touching the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	private void OnTriggerStay(Collider other)
	{
		if (other.tag != "Terrain" && !lastTarget &&
			!Physics.Linecast(turret.transform.position, other.transform.position, LayerMask.GetMask("Terrain")))
			SetLastTarget(other.gameObject);
	}

	private void SetLastTarget(GameObject target)
	{
		lastTarget = target;
		lastTargetPosition = target.transform.position;
		agent.destination = lastTargetPosition;
		agent.isStopped = false;
	}

	public bool TakeDamage(int damage, GameObject source)
	{
		health -= damage;
		if (health <= 0)
		{
			Instantiate(explosionSound, transform.position, Quaternion.identity);
			Instantiate(explosionParticles, transform.position, Quaternion.identity);
			Destroy(gameObject);
			return true;
		}
		if (!lastTarget)
			SetLastTarget(source);

		return false;
	}
}
