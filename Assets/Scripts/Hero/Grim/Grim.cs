using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grim : Hero
{
    // INSPECTOR ATTRIBUTES
    [Header("Hero-based")]
    public GameObject alternateSelf;
    public GameObject grab;
    public GameObject portalPoint;
    public GameObject scytheStorm;
    public float portalPointAngle;
    public float portalPointThrowMag;
    public Color disabledBodyColor;

    // EXTENDED HERO-BASED METHODS
    // Abilities
    protected override IEnumerator Ability1(bool onCooldown, Action callback)
    {
		if (!onCooldown)
		{
			float angle = transform.rotation.y == 0 ? portalPointAngle : 180 - portalPointAngle;
			Vector2 dir = (Vector2)(Quaternion.Euler(0, 0, angle) * Vector2.right);
			GameObject portalPointObj = Instantiate(portalPoint, transform.position, new Quaternion());
			portalPointObj.name = "GrimPortalPoint";
			portalPointObj.GetComponent<HeroAbility>().parentHero = gameObject;
			portalPointObj.GetComponent<Rigidbody2D>().AddForce(dir * portalPointThrowMag);
		}
		callback();
        yield return null;
    }

    protected override IEnumerator Ability2(bool onCooldown, Action callback)
    {
        GameObject wraith = GameObject.Find("GrimWraith");
        if (wraith)
        {
            wraith.GetComponent<GrimWraith>().Terminate(true);
        }
        else if (!onCooldown)
		{
            GameObject altSelfObj = Instantiate(alternateSelf, transform.position, new Quaternion());
            altSelfObj.name = "GrimWraith";
            altSelfObj.GetComponent<Hero>().SetPlayer(playerNumber);
            altSelfObj.GetComponent<GrimWraith>().SetOrigin(gameObject);
            foreach (Transform child in transform) child.parent = altSelfObj.transform;
            SetControl(new HeroControlState {
                jump = false,
                movement = false,
                ability1 = false,
                ability2 = true,
                ultimate = true
            });
            sr.color = disabledBodyColor;
		}
		callback();
        yield return null;
    }

    protected override IEnumerator Ultimate(Action callback)
    {
        GameObject curGrim = gameObject;
        GameObject wraith = GameObject.Find("GrimWraith");
        if (wraith) curGrim = wraith;
        GameObject stormObj = Instantiate(scytheStorm, curGrim.transform.position, new Quaternion());
        stormObj.name = "GrimScytheStorm";
        stormObj.transform.parent = curGrim.transform;
        callback();
        yield return null;
    }
}
