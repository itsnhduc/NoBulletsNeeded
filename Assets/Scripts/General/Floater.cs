using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour {

	public float moveSpeed;
	private Vector2 _direction = Vector2.zero;

	public void SetDirection(Vector2 direction)
	{
		_direction = direction;
	}
	
	void FixedUpdate()
	{
		if (_direction != Vector2.zero)
		{
			transform.position = new Vector2(
				transform.position.x + _direction.x * moveSpeed,
				transform.position.y + _direction.y * moveSpeed
			);
		}
	}
}
