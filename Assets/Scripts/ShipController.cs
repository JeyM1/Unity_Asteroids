using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipController : MonoBehaviour
{
	#region Limitations
	// max speed, rotation speed, boost speed
	[SerializeField]
	float MaxForwardSpeed = 10.0f;
	[SerializeField]
	float MaxAngularSpeed = 2.0f;
	[SerializeField]
	float MaxBoostSpeed = 10.0f;
	#endregion

	#region MovementMultipliers
	// variables to manipulate ship's movement
	[SerializeField]
	float SpeedMultiplier = 1.0f;
	[SerializeField]
	float AngularMultiplier = 3.0f;
	[SerializeField]
	float BoostMultiplier = 2.0f;
	#endregion

	#region DecelerationControls
	// 'Deceleration' for a ship
	[SerializeField]
	float DecelerationMultiplier = 1.0f;
	[SerializeField]
	float DecelerationRotation = 20.0f;
	[SerializeField]
	float DecelerationEpsilon = 0.01f;
	[SerializeField]
	float RotationEpsilon = 1f;
	#endregion


	float _CurrAngularSpeed = 0f;

	Rigidbody2D rigidbody2d;
	GameObject explosionPrefab;

	void Start()
    {
		explosionPrefab = (GameObject)Resources.Load(@"Prefabs/Explosion");
		if (!explosionPrefab)
		{
			Debug.LogError(GetType().Name + ": Error loading explosion prefab.");;
		}

		rigidbody2d = GetComponent<Rigidbody2D>();
	}
	/// <summary>
	/// Fixed frame update
	/// </summary>
    void FixedUpdate()
    {
		#region Controls
		// get input from player
		float speed = Input.GetAxis("Speed");
		float rotation = Input.GetAxis("Rotation");
		float boost = Input.GetAxis("Boost");

		// Rotation
		if (rotation != 0)
		{
			if (Mathf.Abs(_CurrAngularSpeed) < MaxAngularSpeed || Mathf.Sign(_CurrAngularSpeed) != Mathf.Sign(rotation))
				// if reached not reached max speed and player is still holding key
				_CurrAngularSpeed += rotation * AngularMultiplier * Time.deltaTime;
		}
		else
		{
			// decreasing rotation
			if (Mathf.Abs(_CurrAngularSpeed) > RotationEpsilon)
			{
				_CurrAngularSpeed -= _CurrAngularSpeed * DecelerationRotation * Time.deltaTime;
			}
			else _CurrAngularSpeed = 0;
		}
		// Apply rotation to ship
		transform.Rotate(Vector3.back, _CurrAngularSpeed);



		// Forward/Backward
		if (speed != 0 && (rigidbody2d.velocity.magnitude < MaxForwardSpeed))
		{
			// applying force to ship only if there is user input and current ship velocity is less then max speed
			rigidbody2d.AddForce(transform.TransformDirection(Vector2.up) * speed * SpeedMultiplier);
		}
		else // if (boost == 0)
		{
			// decreasing speed to stop ship only if boost is not active
			Vector2 velocity = rigidbody2d.velocity;
			if (velocity.magnitude > DecelerationEpsilon)
			{
				rigidbody2d.AddForce(velocity.normalized * velocity.magnitude * DecelerationMultiplier * -1);
			}
			else
			{
				velocity.x = 0f;
				velocity.y = 0f;
			}

			rigidbody2d.velocity = velocity;

		}

		// Boost
		if (boost > 0)
		{
			if(rigidbody2d.velocity.magnitude < MaxBoostSpeed)
			{
				rigidbody2d.AddForce(transform.TransformDirection(Vector2.up) * BoostMultiplier);
			}
		}

		#endregion
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag.Equals("Asteroid"))
		{
			AudioManager.Play(AudioClipName.ExplosionBig);
			Instantiate(explosionPrefab, transform.position, Quaternion.identity).transform.localScale = new Vector2(4, 4);
			Destroy(gameObject);
			GameManagerSys.OnPlayerShipDestroyed();
		}
	}
	private void Update()
	{
		if (Input.GetButtonDown("Pause"))
		{
			if (GameManagerSys.isGamePaused)
			{
				GameManagerSys.ResumeGame();
			}
			else
			{
				if (!GameManagerSys.isShowingDeathScreen)
				{
					GameManagerSys.PauseGame();
				}
			}
		}
	}
}
