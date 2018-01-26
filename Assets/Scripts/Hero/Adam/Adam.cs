using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Adam : Hero
{
    // INSPECTOR ATTRIBUTES
    [Header("Hero-based")]
    public GameObject pulse;
    public GameObject surge;
    public GameObject orbPrefab;

    // EXTENDED HERO-BASED METHODS
    // Abilities
    protected override IEnumerator Ability1(bool onCooldown, Action callback)
    {
        GameObject curOrb = GameObject.Find("AdamOrb");
        if (curOrb)
        {
            curOrb.GetComponent<AdamOrb>().Activate();
        }
        else if (!onCooldown)
        {
            GameObject orb = Instantiate(orbPrefab, transform.position, new Quaternion());
            orb.name = "AdamOrb";
            orb.GetComponent<HeroAbility>().parentHero = gameObject;
            Vector2 dir = transform.rotation.y == 0 ? Vector2.right : Vector2.left;
            orb.GetComponent<Floater>().SetDirection(dir);
            callback();
        }
        yield return null;
    }
    protected override IEnumerator Ability2(bool onCooldown, Action callback)
    {
        if (!onCooldown)
        {
            pulse.GetComponent<AdamPulse>().Activate(gameObject);
            callback();
        }
        yield return null;
    }
    protected override IEnumerator Ultimate(Action callback)
    {
        float originalMass = rb.mass;
        rb.mass = 1000;
        surge.GetComponent<AdamSurge>().Activate(() =>
        {
            rb.mass = originalMass;
            callback();
        });
        yield return null;
    }
}
