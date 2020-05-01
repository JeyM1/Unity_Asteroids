using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Initializes the game
/// </summary>
public class GameInitializer : MonoBehaviour 
{
	public bool isShipSpawning = true;
	GameObject Ship;
	void Start()
	{
		if (isShipSpawning)
		{
			Ship = (GameObject)Resources.Load(@"Prefabs/BlueShip");
			Instantiate(Ship, Vector3.zero, Quaternion.identity);
		}
	}

	/// <summary>
	/// Awake is called before Start
	/// </summary>
	void Awake()
    {
        // initialize screen utils
        ScreenUtils.Initialize();
    }
}
