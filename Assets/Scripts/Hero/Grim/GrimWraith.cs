using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimWraith : Hero {

    [Header("Hero-based")]
    public float duration;
    public int damage;
    public float damageInterval;
	public List<string> _affected = new List<string> { "Hero" };
    private List<GameObject> _inRange = new List<GameObject>();
	private GameObject _origin;

	void Start()
	{
		base.Start();
		StartCoroutine(TimeDestroy());
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (_affected.Contains(other.tag) && other.name != "Grim")
        {
            if (!_inRange.Contains(other.gameObject)) _inRange.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (_affected.Contains(other.tag))
        {
            _inRange.Remove(other.gameObject);
        }
    }

    IEnumerator TimeDestroy()
    {
        StartCoroutine("DealDamage");
        yield return new WaitForSeconds(duration);
        StopCoroutine("DealDamage");
		Terminate(false);
    }

    IEnumerator DealDamage()
    {
        while (true)
        {
			_inRange.ForEach(obj => {
				obj.GetComponent<Mortality>().AlterHealth(-damage, _origin);
			});
            yield return new WaitForSeconds(damageInterval);
        }
    }

	public void SetOrigin(GameObject origin)
	{
		_origin = origin;
	}

	public void Terminate(bool isTeleport)
	{
		_origin.GetComponent<Hero>().SetControl(true);
		_origin.GetComponent<SpriteRenderer>().color = Color.white;
		_origin.GetComponent<Rigidbody2D>().velocity = rb.velocity;
		_origin.transform.position = transform.position;
		foreach (Transform child in transform) child.parent = _origin.transform;
		Destroy(gameObject);
	}
}
