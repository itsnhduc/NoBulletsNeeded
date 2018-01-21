using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControl : MonoBehaviour {

	public float multiplier = 1.5f;

	void Start () {
		Physics2D.gravity = Physics2D.gravity * multiplier;
	}

	public void ChangeDirection(Vector2 dir) {
		Physics2D.gravity = dir.normalized * Physics2D.gravity.magnitude * multiplier;
	}
}
