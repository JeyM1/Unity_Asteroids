using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Initializes the game
/// </summary>
public class GameInitializer : MonoBehaviour 
{
	public bool isShipSpawning = true;
	GameObject ShipPrefab;


	void Start()
	{
		if (isShipSpawning)
		{
			ShipPrefab = (GameObject)Resources.Load(@"Prefabs/BlueShip");
			Instantiate(ShipPrefab, Vector3.zero, Quaternion.identity);
		}
	}

	void Awake()
    {
        ScreenUtils.Initialize();
		GameManagerSys.Initialize();
	}
}
