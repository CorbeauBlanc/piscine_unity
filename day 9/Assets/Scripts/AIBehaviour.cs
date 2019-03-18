using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBehaviour : MonoBehaviour
{
	public int life = 10;
	public Animator animator;
	public GameObject zombieCollider;

	private PlayerBehaviour player;
	private NavMeshAgent navMeshAgent;
	private GameObject currentTarget;
	private int lastLoopNb = -1;
	private bool isDead = false;
	private bool goTowardsCenter = true;

	private void Start()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
		navMeshAgent.destination = new Vector3(50, 360, 50);
		player = PlayerBehaviour.instance;
	}

	private void Attack()
	{
		float animationTime;
		int currentLoopNb;

		navMeshAgent.isStopped = true;
		animator.SetBool("Is Attacking", true);
		if (!animator.GetCurrentAnimatorStateInfo(0).IsName("AttackEnemy"))
			return;
		animationTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
		currentLoopNb = (int)animationTime;
		if (animationTime - currentLoopNb > .3 && currentLoopNb > lastLoopNb)
		{
			lastLoopNb = currentLoopNb;
			if (player.LooseLife())
				currentTarget = null;
		}
	}

	private void Update()
	{
		if (isDead && animator.GetCurrentAnimatorStateInfo(0).IsName("Death") && (int)animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
			Destroy(gameObject);
		else if (!isDead)
		{
			animator.SetBool("Is Running", goTowardsCenter || (currentTarget != null && navMeshAgent.remainingDistance > 2.6));
			if (currentTarget)
			{
				navMeshAgent.destination = currentTarget.transform.position;
				if (navMeshAgent.remainingDistance <= 2.6)
					Attack();
				else
					navMeshAgent.isStopped = false;
			}
		}
	}

	private void Die()
	{
		isDead = true;
		Destroy(zombieCollider);
		navMeshAgent.enabled = false;
		animator.SetTrigger("Die");
	}

	private void targetPlayer()
	{
		goTowardsCenter = false;
		currentTarget = player.gameObject;
		navMeshAgent.destination = player.transform.position;
		navMeshAgent.isStopped = false;
		animator.SetBool("Is Running", true);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "player")
			targetPlayer();
	}

	public void TakeHit(int nb)
	{
		life -= nb;
		if (life <= 0)
			Die();
		else if (!isDead)
			targetPlayer();
	}
}
