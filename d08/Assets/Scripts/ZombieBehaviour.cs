using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBehaviour : MonoBehaviour {
	public int health = 3;
	public SpawnerBehaviour sourceSpawner = null;
	public GameObject zombieCollider;

	private PlayerBehaviour target = null;
	private NavMeshAgent agent;
	private Animator animator;
	private bool isDead = false;
	private Vector3 deathPoint;

	public bool TakeDamage()
	{
		health--;
		if (health <= 0)
			Die();
		return (health <= 0);
	}

	private void Die()
	{
		Destroy(zombieCollider);
		target = null;
		animator.SetTrigger("Die");
		agent.enabled = false;
		if (sourceSpawner)
			sourceSpawner.nbInstantiatedZombies = Math.Max(0, sourceSpawner.nbInstantiatedZombies - 1);
		isDead = true;
		deathPoint = transform.position;
	}

	private void Attack()
	{
		agent.isStopped = true;
		animator.SetBool("Is Attacking", true);
	}

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		animator = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		if (isDead)
		{
			if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death") && (int)animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
			{
				if (deathPoint.y - transform.position.y < 3)
					transform.Translate(Vector3.down * Time.deltaTime);
				else
					Destroy(gameObject);
			}
		}
		else
		{
			animator.SetBool("Is Running", target != null && agent.remainingDistance > 1.5);

			if (target)
			{
				agent.destination = target.transform.position;
				if (agent.remainingDistance <= 1.5)
					Attack();
				else
					agent.isStopped = false;
			}
		}
	}

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			agent.destination = other.transform.position;
			target = other.GetComponent<PlayerBehaviour>();
		}
	}
}
