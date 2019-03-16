using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBehaviour : MonoBehaviour {

	public NavMeshAgent agent;
	public Animator animator;
	public Vector3 cameraOffset;

	private ZombieBehaviour target = null;
	private int lastLoopNumber = -1;
	private bool isAttacking = false;

	private void Attack()
	{
		int currentLoopNumber;
		if (!animator.GetCurrentAnimatorStateInfo(0).IsName("AttackMaya"))
			return;
		currentLoopNumber = (int)animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
		if (currentLoopNumber > lastLoopNumber)
		{
			lastLoopNumber = currentLoopNumber;
			if (target.TakeDamage())
				target = null;
			isAttacking = false;
		}
	}

	private void ProcessInputs()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, LayerMask.GetMask("Zombie", "Terrain")))
			{
				if (hit.collider.tag == "Ennemi")
					target = hit.collider.GetComponentInParent<ZombieBehaviour>();
				else
					target = null;
				agent.isStopped = false;
				agent.destination = hit.point;
			}
		}
		if (Input.GetButton("Fire1") && target)
			isAttacking = true;
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	private void Update()
	{
		Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, transform.position + cameraOffset, Time.deltaTime);

		if (target)
		{
			agent.destination = target.transform.position;
			if (agent.remainingDistance <= 1.5 && isAttacking)
			{
				animator.SetBool("Is Running", false);
				animator.SetBool("Is Attacking", true);
				agent.isStopped = true;
				transform.LookAt(target.transform);
				Attack();
			}
			else
				animator.SetBool("Is Running", true);
		}
		else
			animator.SetBool("Is Running", !agent.isStopped && agent.remainingDistance > 1);

		ProcessInputs();

		if (!isAttacking)
		{
			target = null;
			animator.SetBool("Is Attacking", false);
		}

		if (!target)
			lastLoopNumber = -1;
	}


}
