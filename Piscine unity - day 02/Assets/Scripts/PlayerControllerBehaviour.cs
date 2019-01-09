using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerBehaviour : MonoBehaviour
{
	public static PlayerControllerBehaviour instance { get; private set; }
	public delegate void MoveEvent();
	public event MoveEvent OnMoveToLocation;

	private List<FootmanBehavior> selectedFootmans;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		selectedFootmans = new List<FootmanBehavior>();
	}

	private void addFootmanToSelection(FootmanBehavior ftm)
	{
		ftm.selected = true;
		ftm.main = true;
		if (selectedFootmans.Count > 0)
			selectedFootmans[selectedFootmans.Count - 1].main = false;
		selectedFootmans.Add(ftm);
		HumanAudioManager.instance.playRandomSelected();
	}

	private void clearSelection()
	{
		if (selectedFootmans.Count > 0)
			selectedFootmans[selectedFootmans.Count - 1].main = false;
		foreach (FootmanBehavior footman in selectedFootmans)
			footman.selected = false;

		selectedFootmans.Clear();
	}

	// Update is called once per frame
	void Update()
	{
		int i;
		FootmanBehavior tmp;

		if (Input.GetButtonDown("Fire1"))
		{
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1,
												LayerMask.GetMask("Characters"));
			if (hit.collider)
			{
				if (!Input.GetKey("left ctrl"))
					clearSelection();
				tmp = hit.collider.GetComponent<FootmanBehavior>();
				if ((i = selectedFootmans.IndexOf(tmp)) < 0)
					addFootmanToSelection(tmp);
			} else
				OnMoveToLocation();
		}
		if (Input.GetButtonDown("Fire2"))
			clearSelection();
	}
}
