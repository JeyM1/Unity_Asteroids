using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	GameObject bulletPrefab;

	[SerializeField]
	float BulletSpeed = 5f;

	Transform FirePoint;

	// cached for efficiency
	AsteroidSpawner asteroidSpawner;
	void Start()
    {
		bulletPrefab = (GameObject)Resources.Load(@"Prefabs/Bullet");
		if (!bulletPrefab)
		{
			Debug.LogError(GetType().Name + ": Error loading bullet prefab."); ;
		}

		FirePoint = transform.GetChild(0);

		asteroidSpawner = GameObject.FindGameObjectWithTag("AsteroidSpawner").GetComponent<AsteroidSpawner>();
	}

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire"))
		{
			GameObject bullet = Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
			bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.TransformDirection(Vector2.up) * BulletSpeed, ForceMode2D.Impulse);
		}
	}
}
