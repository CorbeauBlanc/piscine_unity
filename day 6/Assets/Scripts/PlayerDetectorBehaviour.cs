using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectorBehaviour : MonoBehaviour
{

	public GameObject detector;
	public bool enableToggler = true;

    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(toggleDetector());
    }

	private IEnumerator toggleDetector()
	{
		while (enableToggler)
		{
			yield return new WaitForSeconds(10);
			detector.SetActive(false);
			yield return new WaitForSeconds(5);
			detector.SetActive(true);
		}
	}

}
