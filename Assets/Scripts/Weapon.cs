using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	GameObject bulletPrefab;
	Rigidbody2D ship_rgbd;

	[SerializeField]
	float BulletSpeed = 5f;

	Transform FirePoint;
	void Start()
    {
		bulletPrefab = (GameObject)Resources.Load(@"Prefabs/Bullet");
		if (!bulletPrefab)
		{
			Debug.LogError(GetType().Name + ": Error loading bullet prefab."); ;
		}

		FirePoint = transform.GetChild(0);
		ship_rgbd = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire") && !GameManagerSys.isGamePaused)
		{
			Vector2 shipSpeed = ship_rgbd.velocity;

			AudioManager.Play(AudioClipName.Fire1);
			GameObject bullet = Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
			bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.TransformDirection(Vector2.up) * BulletSpeed + new Vector3(shipSpeed.x, shipSpeed.y), ForceMode2D.Impulse);
		}
	}
}
