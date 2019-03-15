using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBehaviour : MonoBehaviour {

	public NavMeshAgent agent;
	public Animator animator;
	public Vector3 cameraOffset;

	public int health = 100;

	private void Die()
	{
		animator.SetTrigger("Die");
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	private void Update()
	{
		Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, transform.position + cameraOffset, Time.deltaTime);
		animator.SetBool("Is Running", agent.remainingDistance > 1);
		if (Input.GetButtonUp("Fire1"))
		{
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, LayerMask.GetMask("Terrain")))
				agent.destination = hit.point;
		}
	}


}
