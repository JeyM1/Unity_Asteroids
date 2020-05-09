using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField]
	float lifeTimeDurationSeconds = 2f;

	GameObject explosionPrefab;

	Timer lifeTime;
	void Start()
	{
		explosionPrefab = (GameObject)Resources.Load(@"Prefabs/Explosion");
		if (!explosionPrefab)
		{
			Debug.LogError(GetType().Name + ": Error loading explosion prefab."); ;
		}
		lifeTime = gameObject.AddComponent<Timer>();
		lifeTime.Duration = lifeTimeDurationSeconds;
		lifeTime.Run();

	}

	void Update()
	{
		if (lifeTime.Finished)
		{
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Asteroid"))
		{
			Detonate();
			Destroy(collision.gameObject);
		}
	}

	void Detonate()
	{
		Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
