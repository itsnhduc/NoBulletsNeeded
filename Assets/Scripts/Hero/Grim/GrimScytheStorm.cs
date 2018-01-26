using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimScytheStorm : HeroAbility {

    private List<string> _affected = new List<string> { "Hero" };
    public float duration;
    public int damage;
    public float damageInterval;
    private List<GameObject> _inRange = new List<GameObject>();

    void Start()
    {
        parentHero = transform.parent.gameObject;
        StartCoroutine(TimeDestroy());
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (_affected.Contains(other.tag) && other.name != parentHero.name)
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
		Destroy(gameObject);
    }

    IEnumerator DealDamage()
    {
        while (true)
        {
			_inRange.ForEach(obj => {
				obj.GetComponent<Mortality>().AlterHealth(-damage, parentHero);
			});
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
