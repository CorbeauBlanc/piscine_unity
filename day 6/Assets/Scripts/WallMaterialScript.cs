using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMaterialScript : MonoBehaviour {

	public float xScale;
	private Renderer rend;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		rend = gameObject.GetComponent<Renderer>();
		rend.material.mainTextureScale = new Vector2(transform.localScale.x * xScale, rend.material.mainTextureScale.y);
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		rend.material.mainTextureScale = new Vector2(transform.localScale.x * xScale, rend.material.mainTextureScale.y);
	}
}
