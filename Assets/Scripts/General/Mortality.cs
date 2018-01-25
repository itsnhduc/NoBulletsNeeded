using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortality : MonoBehaviour
{
    public int maxHealth;
    public float ultChargePerDamage;
    public int health { get; private set; }
    private static int minTimePass = 5;

    private List<HealthExchange> _dealerList = new List<HealthExchange>();

    void Start()
    {
        health = maxHealth;
    }

    public void AlterHealth(int offset, GameObject dealer)
    {
        // update health
        int prevHealth = health;
        health += offset;

        // save record
        HealthExchange curExchange = new HealthExchange
        {
            timestamp = DateTime.Now,
            dealer = dealer,
            receiver = gameObject,
            offset = offset,
            isFinalBlow = health <= 0,
            isSelf = false
        };
        bool wasHurtByOther = false;
        if (dealer.tag != "KillZone") wasHurtByOther = true;
        else if (dealer.tag == "KillZone" && _dealerList.Count >= 1)
        {
            HealthExchange lastExchange = _dealerList[_dealerList.Count - 1];
            int timePassed = (DateTime.Now - lastExchange.timestamp).Seconds;
            if (timePassed < minTimePass && lastExchange.dealer.name != gameObject.name)
            {
                curExchange.dealer = lastExchange.dealer;
                wasHurtByOther = true;
            }
        }
        if (!wasHurtByOther) curExchange.isSelf = true;
        _dealerList.Add(curExchange);

        // after effect
        if (health > maxHealth) health = maxHealth;
        if (health < 0) health = 0;
        if (health == 0) StartCoroutine(TimedDestroy());

        // give ult charge
        GameObject dealerHero = curExchange.dealer.tag == "Hero" ? curExchange.dealer : null;
        if (dealerHero)
        {
            float dealerUltChargePerDmg = dealerHero.GetComponent<Mortality>().ultChargePerDamage;
            float ultChargeReward = Math.Abs(prevHealth - health) * dealerUltChargePerDmg;
            dealerHero.GetComponent<Hero>().GainUltCharge(ultChargeReward);
        }

    }

    public void Kill(GameObject dealer)
    {
        AlterHealth(-maxHealth, dealer);
    }

    public HealthExchange GetKiller()
    {
        return _dealerList.Find(dealer => dealer.isFinalBlow);
    }

    IEnumerator TimedDestroy()
    {
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }
}