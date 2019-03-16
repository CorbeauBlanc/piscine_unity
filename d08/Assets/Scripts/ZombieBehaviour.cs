using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBehaviour : MonoBehaviour {
	public int health = 3;

	private PlayerBehaviour target = null;
	private NavMeshAgent agent;
	private Animator animator;

	public bool TakeDamage()
	{
		health--;
		if (health <= 0)
		{
			target = null;
			animator.SetTrigger("Die");
			agent.enabled = false;
		}
		return health <= 0;
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
