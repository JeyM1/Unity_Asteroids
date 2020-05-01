using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
	CircleCollider2D colider;

	void Start()
	{
		colider = GetComponent<CircleCollider2D>();
		if (!colider)
			Debug.LogError("Circle colider isn't attached to object!");
	}
	void OnBecameInvisible()
	{
		Vector2 newPosition = transform.position;
		if(newPosition.x < ScreenUtils.ScreenLeft || newPosition.x > ScreenUtils.ScreenRight)
		{
			newPosition.x = -newPosition.x + Mathf.Sign(newPosition.x) * colider.radius;
		}
		if (newPosition.y < ScreenUtils.ScreenBottom || newPosition.y > ScreenUtils.ScreenTop)
		{
			newPosition.y = -newPosition.y + Mathf.Sign(newPosition.y) * colider.radius;
		}
		transform.position = newPosition;
	}
	
}
