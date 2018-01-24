using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortality : MonoBehaviour
{
    public int maxHealth;
	public float ultChargePerDamage;
    public int health { get; private set; }

    private List<HealthDealer> _dealerList = new List<HealthDealer>();

    void Start()
    {
        health = maxHealth;
    }

    public void AlterHealth(int offset, GameObject dealer)
    {
		// update health
        health += offset;

		// save record
        _dealerList.Add(new HealthDealer
        {
			timestamp = DateTime.Now,
			dealer = dealer,
			offset = offset,
			isFinalBlow = health <= 0
        });
		
		// give ult charge
		GameObject dealerHero = dealer.tag == "Hero" ? dealer : dealer.transform.parent.gameObject;
		dealerHero.GetComponent<Hero>().GainUltCharge(Math.Abs(offset) * ultChargePerDamage);

		// after effect
        if (health > maxHealth) health = maxHealth;
        if (health <= 0) StartCoroutine(TimedDestroy());
    }

    public void Kill(GameObject dealer)
    {
        AlterHealth(-maxHealth, dealer);
    }

    IEnumerator TimedDestroy()
    {
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }
}

public class HealthDealer
{
    public DateTime timestamp { get; set; }
    public GameObject dealer { get; set; }
    public int offset { get; set; }
    public bool isFinalBlow { get; set; }
}