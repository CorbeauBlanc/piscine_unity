using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDHandler : MonoBehaviour
{
	public GameObject weapon;
	public GameObject rangeCircle;

	private GameObject instantiatedRangeCircle;

	public void beginDrag()
	{
		instantiatedRangeCircle = Instantiate(rangeCircle, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
		instantiatedRangeCircle.transform.localScale *= weapon.GetComponent<CircleCollider2D>().radius;
	}

	public void dragging()
	{
		instantiatedRangeCircle.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	public void dropping()
	{
		Destroy(instantiatedRangeCircle);
	}

}