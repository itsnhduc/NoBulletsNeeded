using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flasher : MonoBehaviour {

	private SpriteRenderer _sr;
	public float interval;

	void Start()
	{
		_sr = GetComponent<SpriteRenderer>();
	}
	public void Flash() {
		StartCoroutine("StartFlash");
	}

	IEnumerator StartFlash() {
		_sr.enabled = true;
		yield return new WaitForSeconds(interval);
		_sr.enabled = false;
	}
}
