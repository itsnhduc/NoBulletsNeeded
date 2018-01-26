using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimGrab : HeroAbility
{
    public float duration;
    public int damage;
    public float damageInterval;
    private List<string> _affected = new List<string> { "Hero" };
    private List<GameObject> _inRange = new List<GameObject>();

    void Start()
    {
        StartCoroutine(TimeDestroy());
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (_affected.Contains(other.tag) && other.name != parentHero.name)
        {
            if (!_inRange.Contains(other.gameObject)) _inRange.Add(other.gameObject);
            other.GetComponent<Hero>().SetControl(new HeroControlState
            {
                jump = false,
                movement = true,
                ability1 = false,
                ability2 = false,
                ultimate = false,
            });
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (_affected.Contains(other.tag))
        {
            _inRange.Remove(other.gameObject);
            other.GetComponent<Hero>().SetControl(true);
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
