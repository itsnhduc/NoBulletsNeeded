using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBlizzard : MonoBehaviour {

	public float duration;
	public float speedMultiplier;
	public PhysicsMaterial2D snowMaterial;
	public PhysicsMaterial2D groundMaterial;

	void Start () {
		StartCoroutine(_Start());
	}

	private IEnumerator _Start()
	{
		_ApplyGlobalEffect(true);
		yield return new WaitForSeconds(duration);
		_ApplyGlobalEffect(false);
		Destroy(gameObject);
	}

	private void _ApplyGlobalEffect(bool isStart)
	{
		foreach (GameObject ground in GameObject.FindGameObjectsWithTag("Ground"))
		{
			PhysicsMaterial2D targetMaterial = isStart ? snowMaterial : groundMaterial;
			ground.GetComponent<BoxCollider2D>().sharedMaterial = targetMaterial;
		}
		foreach (GameObject hero in GameObject.FindGameObjectsWithTag("Hero"))
		{
			float multiplier = isStart ? speedMultiplier : (1 / speedMultiplier);
			Hero heroInfo = hero.GetComponent<Hero>();
			heroInfo.groundMoveSpeed *= multiplier;
			heroInfo.airMoveSpeed *= multiplier;
		}
	}
}
