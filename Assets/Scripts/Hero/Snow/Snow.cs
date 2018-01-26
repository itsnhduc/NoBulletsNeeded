using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : Hero
{
    // INSPECTOR ATTRIBUTES
    [Header("Hero-based")]
    public GameObject frostBlast;
    public GameObject blizzard;
    // INTERNAL ATTRIBUTES
    // EXTENDED HERO-BASED METHODS
    // Abilities
    protected override IEnumerator Ability1(bool onCooldown, Action callback)
    {
        if (!onCooldown)
        {
            GameObject frostBlastObj = Instantiate(frostBlast, transform.position, new Quaternion());
            frostBlastObj.name = "SnowFrostBlast";
            frostBlastObj.GetComponent<HeroAbility>().parentHero = gameObject;
            float yRotation = transform.rotation.y == 0 ? 0 : 180;
            frostBlastObj.transform.rotation = Quaternion.Euler(0, yRotation, 0);
            Vector2 dir = transform.rotation.y == 0 ? Vector2.right : Vector2.left;
            frostBlastObj.GetComponent<Floater>().SetDirection(dir);
            callback();
        }
        yield return null;
    }
    protected override IEnumerator Ability2(bool onCooldown, Action callback)
    {
        if (!onCooldown)
        {
			frostBlast.GetComponent<SnowFrostBlast>().GiveCryo(gameObject);
			callback();
        }
		yield return null;
    }
    protected override IEnumerator Ultimate(Action callback)
    {
        Instantiate(blizzard);
        callback();
		yield return null;
    }
}
