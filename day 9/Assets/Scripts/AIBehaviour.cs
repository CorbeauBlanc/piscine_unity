using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBehaviour : MonoBehaviour
{
	public GameObject player;
	public int life = 10;
	public NavMeshAgent navMeshAgent;
	public Animator animator;

	private Vector3 currentTarget;
	private float currentTime = 0;

	private void Start()
	{
		navMeshAgent.destination = new Vector3(50, 50, 0);
	}

	private void Update()
	{
		currentTime += Time.deltaTime;

		if (Vector3.Distance(transform.position, currentTarget) < 5 && currentTime > .5)
		{
			navMeshAgent.isStopped = true;
			animator.SetBool("Walk", false);
			player.GetComponent<PlayerBehaviour>().looseLife();
			currentTime = 0;
		}
		if (Vector3.Distance(transform.position, currentTarget) < 1)
		{
			navMeshAgent.isStopped = true;
			animator.SetBool("Walk", false);
		}
	}

	public void takeHit(int nb)
	{
		life -= nb;
		currentTarget = player.transform.position;
		navMeshAgent.destination = currentTarget;
		navMeshAgent.isStopped = false;
		animator.SetBool("Walk", true);
		if (life <= 0)
			Destroy(gameObject);
	}

	private void targetPlayer(Collider other)
	{
		if (other.tag == "player" && Vector3.Distance(other.transform.position, currentTarget) < 5)
		{
			navMeshAgent.isStopped = false;
			currentTarget = other.transform.position;
			navMeshAgent.destination = currentTarget;
			animator.SetBool("Walk", true);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		targetPlayer(other);
	}

	private void OnTriggerStay(Collider other)
	{
		targetPlayer(other);
	}
}
