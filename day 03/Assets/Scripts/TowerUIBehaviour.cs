using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUIBehaviour : MonoBehaviour
{
	public GameObject tower;
	public GameObject rangeCircle;
	public Image towerSprite;
	public Text towerInfos;

	private GameObject instantiatedRangeCircle;
	private towerScript towerBehaviour;
	private bool available = true;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		towerBehaviour = tower.GetComponent<towerScript>();
		towerInfos.text = "Cost: " + towerBehaviour.energy + "\nDamages: " + towerBehaviour.damage +
						"\nFire Rate: " + towerBehaviour.fireRate + "\nRange: " + towerBehaviour.range;
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		if (gameManager.gm.playerEnergy < towerBehaviour.energy)
		{
			if (available)
			{
				towerSprite.color = Color.red;
				available = false;
			}
		}
		else
		{
			if (!available)
			{
				towerSprite.color = Color.white;
				available = true;
			}
		}
	}

	public void beginDrag()
	{
		if (!available)
			return;
		instantiatedRangeCircle = Instantiate(rangeCircle, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
		instantiatedRangeCircle.transform.localScale *= towerBehaviour.range * 2;
	}

	public void dragging()
	{
		if (!available || !instantiatedRangeCircle)
			return;
		instantiatedRangeCircle.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	public void dropping()
	{
		if (!available || !instantiatedRangeCircle)
			return;
		Destroy(instantiatedRangeCircle);
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1,
											LayerMask.GetMask("floortiles"));
		if (!hit || hit.collider.tag != "empty")
			return;
		hit.collider.tag = "tower";
		Instantiate(tower, hit.transform.position, Quaternion.identity);
		gameManager.gm.playerEnergy -= towerBehaviour.energy;
	}

}
