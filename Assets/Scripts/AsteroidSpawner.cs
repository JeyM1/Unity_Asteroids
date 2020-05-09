using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AsteroidSpawner : MonoBehaviour
{
	[SerializeField]
	List<GameObject> Asteroids = new List<GameObject>();
	[SerializeField, Range(0, 100)]
	int MaxAsteroidCount = 0;

	int currentBigAsteroidCount;

	[SerializeField, Range(0, 50)]
	float MinImpulseForce = 2f;
	[SerializeField, Range(0, 50)]
	float MaxImpulseForce = 5f;

	const int MaxSpawnTries = 50;

	GameManagerSys gameManager;

	UnityEvent m_EventAsteroidDestroyed = new UnityEvent();
	public UnityEvent EventAsteroidDestroyed
	{
		get { return m_EventAsteroidDestroyed; }
	}

	/* Cached locations for asteroid spawning:
	 *	- 0: Top
	 *	- 1: Right
	 *	- 2: Bottom
	 *	- 3: Left
	 */
	Vector2[] locations;
	float[] reflocations = { -1, Mathf.PI/2, 1, 0.5f };

	void Start()
    {
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerSys>();
		currentBigAsteroidCount = 0;
		// DEBUG
		if (Asteroids.Count == 0)
		{
			Debug.LogError(GetType().Name + ": at least 1 prefab needs to be added in Asteroids.");
		}

		locations = new Vector2[4];
		// Top
		locations[0] = new Vector2(0, ScreenUtils.ScreenTop);
		// Right
		locations[1] = new Vector2(ScreenUtils.ScreenRight, 0);
		// Bottom
		locations[2] = new Vector2(0, ScreenUtils.ScreenBottom);
		// Left
		locations[3] = new Vector2(ScreenUtils.ScreenLeft, 0);

		EventAsteroidDestroyed.AddListener(gameManager.UpdateScore);

		for (int i = 0; i < MaxAsteroidCount; i++)
		{
			SpawnNewRandomAsteroid();
		}
	}

	void SpawnNewRandomAsteroid()
	{
		GameObject asteroidToSpawn = Asteroids[Random.Range(0, Asteroids.Count)];
		CircleCollider2D asteroidColider = asteroidToSpawn.GetComponent<CircleCollider2D>();

		int chosenLocation = Random.Range(0, locations.Length);

		Vector2 location = locations[chosenLocation];

		// checking is area is free and then spawning
		for (int i = 0; i < MaxSpawnTries && Physics2D.OverlapCircle(location + asteroidColider.offset, asteroidColider.radius); i++)
		{
			chosenLocation = Random.Range(0, locations.Length);
			location = locations[chosenLocation];
		}

		if(!Physics2D.OverlapCircle(location + asteroidColider.offset, asteroidColider.radius))
		{
			// Calculating Impulse Force for asteroid
			float angle = Random.Range(0, Mathf.PI) * reflocations[chosenLocation];
			Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
			float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);

			// Instantiating and applying force
			GameObject tmp = Instantiate(asteroidToSpawn, location, Quaternion.identity);
			tmp.GetComponent<Asteroid>().AsteroidSpawner = this;
			tmp.GetComponent<Asteroid>().IsInMainMenu = gameManager.isInMainMenu;
			tmp.GetComponent<Rigidbody2D>().AddForce(direction * magnitude, ForceMode2D.Impulse);
			currentBigAsteroidCount++;
		}
	}

	public void OnBigAsteroidDestroyed()
	{
		currentBigAsteroidCount--;
		if(currentBigAsteroidCount < MaxAsteroidCount)
			SpawnNewRandomAsteroid();
	}
}