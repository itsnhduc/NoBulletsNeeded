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
    public float orbSpeed;
    public float pulseMag;
    public int pulseDamage;

    // INTERNAL ATTRIBUTES
    // Movements
    private readonly List<string> _affected = new List<string> { "Hero", "Interactive" };
    // Abilities
    private List<GameObject> _inRangeObjs = new List<GameObject>();

    // EXTENDED HERO-BASED METHODS
    // Monobehavior
    protected override IEnumerator HandleTriggerStay(Collider2D other, Action callback)
    {
        if (_affected.Contains(other.tag) && !_inRangeObjs.Contains(other.gameObject))
        {
            _inRangeObjs.Add(other.gameObject);
        }
        callback();
        yield return null;
    }
    protected override IEnumerator HandleTriggerExit(Collider2D other, Action callback)
    {
        if (_affected.Contains(other.tag) && _inRangeObjs.Contains(other.gameObject))
        {
            _inRangeObjs.Remove(other.gameObject);
        }
        callback();
        yield return null;
    }
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
            GameObject orb = Instantiate(orbPrefab, transform);
            orb.name = "AdamOrb";
            Vector2 dir = rb.transform.rotation.y == 0 ? Vector2.right : Vector2.left;
            orb.GetComponent<Rigidbody2D>().AddForce(dir * orbSpeed);
            callback();
        }
        yield return null;
    }
    protected override IEnumerator Ability2(bool onCooldown, Action callback)
    {
        if (!onCooldown)
        {
            _inRangeObjs.ForEach(obj =>
            {
                Vector2 diff = obj.transform.position - transform.position;
                obj.GetComponent<Rigidbody2D>().AddForce(diff.normalized * pulseMag);
                Mortality mort = obj.GetComponent<Mortality>();
                if (mort) mort.AlterHealth(-pulseDamage, gameObject);
            });
            pulse.GetComponent<Flasher>().Flash();
            callback();
        }
        yield return null;
    }
    protected override IEnumerator Ultimate(float ultCharge, Action callback)
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
