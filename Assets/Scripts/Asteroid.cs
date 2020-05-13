using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
	public List<GameObject> childsAsteroids;
	float circleColliderRadius;

	private bool isQuitting = false;

	private AsteroidSpawner asteroidSpawner;
	public AsteroidSpawner AsteroidSpawner
	{
		set
		{
			asteroidSpawner = value;
		}
	}


	Vector2 speed;

	void OnApplicationQuit()
	{
		isQuitting = true;
	}

	void Start()
	{
		circleColliderRadius = GetComponent<CircleCollider2D>().radius;
		speed = GetComponent<Rigidbody2D>().velocity;
	}

	void OnDestroy()
	{
		if (!isQuitting && !GameManagerSys.isInMainMenu && !GameManagerSys.isSwitchingLevel)
		{
			Vector2 currentPos = transform.position;
			foreach (GameObject asteroid in childsAsteroids)
			{
				Vector2 position = new Vector2(
											currentPos.x + Random.Range(-circleColliderRadius, circleColliderRadius),
											currentPos.y + Random.Range(-circleColliderRadius, circleColliderRadius)
											);
				GameObject tmp = Instantiate(asteroid, position, Quaternion.identity);
				tmp.GetComponent<Rigidbody2D>().velocity = new Vector2(speed.x + Random.Range(-0.5f, 0.5f), speed.y + Random.Range(-0.5f, 0.5f));
				tmp.GetComponent<Asteroid>().AsteroidSpawner = asteroidSpawner;
			}
			GameManagerSys.IncrementScore();
			if(childsAsteroids.Count != 0)
			{
				asteroidSpawner.OnBigAsteroidDestroyed();
			}
		}
	}
}
