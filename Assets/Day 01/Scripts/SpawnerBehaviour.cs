using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnerBehaviour : MonoBehaviour
{

	public GameObject[]	characters;

	private List<GameObject> spawnedCharacters;

	private void Start()
	{
		spawnedCharacters = new List<GameObject>();
	}

	private void ResetScene()
	{
		foreach (GameObject spawned in spawnedCharacters)
			GameObject.Destroy(spawned);

	}

	// Update is called once per frame
	void Update()
    {
		int num = -1;

		if (Input.GetKeyUp("1"))
			num = 0;
		else if (Input.GetKeyUp("2"))
			num = 1;
		else if (Input.GetKeyUp("3"))
			num = 2;
		else if (Input.GetKeyUp("r") || Input.GetKeyUp("backspace"))
			ResetScene();

		if (num != -1)
		{
			GameObject selected = null;
			foreach (GameObject spawned in spawnedCharacters)
			{
				CharacterBehaviour tmp = spawned.GetComponent<CharacterBehaviour>();
				if (num != tmp.Index)
				{
					tmp.Active = false;
					tmp = null;
				} else
					selected = spawned;
			}

			if (!selected)
			{
				selected = Instantiate(characters[num], transform.position, Quaternion.identity) as GameObject;
				spawnedCharacters.Add(selected);
				selected.GetComponent<CharacterBehaviour>().Index = num;
			}
			selected.GetComponent<CharacterBehaviour>().Active = true;
		}
	}
}
