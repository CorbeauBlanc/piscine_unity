﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBehaviour : MonoBehaviour {

	public BoxCollider2D upTrigger;
	public BoxCollider2D downTrigger;
	public BoxCollider2D leftTrigger;
	public BoxCollider2D rightTrigger;

	private bool upTriggerActivated = false;
	private bool downTriggerActivated = false;
	private bool leftTriggerActivated = false;
	private bool rightTriggerActivated = false;

	private bool isCharacterInExit = false;
	private CharacterBehaviour characterInstance;

	public string charactersTag;

	// Update is called once per frame
	void Update () {
		if (!isCharacterInExit && upTriggerActivated && downTriggerActivated && leftTriggerActivated && rightTriggerActivated &&
			characterInstance.isGrounded())
		{
			characterInstance.transform.position = gameObject.transform.position;
			characterInstance.isInExit = true;
			SpawnerBehaviour.Instance.addExitedCharacter();
		}
	}

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == charactersTag)
		{
			if (!characterInstance)
				characterInstance = other.gameObject.GetComponent<CharacterBehaviour>();
			if (!upTriggerActivated && upTrigger.IsTouching(other))
				upTriggerActivated = true;
			else if (!downTriggerActivated && downTrigger.IsTouching(other))
				downTriggerActivated = true;
			else if (!leftTriggerActivated && leftTrigger.IsTouching(other))
				leftTriggerActivated = true;
			else if (!rightTriggerActivated && rightTrigger.IsTouching(other))
				rightTriggerActivated = true;
		}
	}

	/// <summary>
	/// Sent when another object leaves a trigger collider attached to
	/// this object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == charactersTag)
		{
			if (upTriggerActivated && !upTrigger.IsTouching(other))
				upTriggerActivated = false;
			else if (downTriggerActivated && !downTrigger.IsTouching(other))
				downTriggerActivated = false;
			else if (leftTriggerActivated && !leftTrigger.IsTouching(other))
				leftTriggerActivated = false;
			else if (rightTriggerActivated && !rightTrigger.IsTouching(other))
				rightTriggerActivated = false;
		}
	}
}
