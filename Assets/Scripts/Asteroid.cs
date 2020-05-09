using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
	public List<GameObject> childsAsteroids;
	float circleColliderRadius;

	private bool isQuitting = false;
	private bool isInMainMenu;

	public bool IsInMainMenu
	{
		set
		{
			this.isInMainMenu = value;
		}
	}

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
		//asteroidSpawner = GameObject.FindGameObjectWithTag("AsteroidSpawner").GetComponent<AsteroidSpawner>();
		circleColliderRadius = GetComponent<CircleCollider2D>().radius;
		speed = GetComponent<Rigidbody2D>().velocity;
		//isInMainMenu = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerSys>().isInMainMenu;
	}

	void OnDestroy()
	{
		if (!isQuitting && !isInMainMenu)
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
			asteroidSpawner.EventAsteroidDestroyed.Invoke();
			if(childsAsteroids.Count == 0)
			{
				asteroidSpawner.OnBigAsteroidDestroyed();
			}
		}
	}
}
