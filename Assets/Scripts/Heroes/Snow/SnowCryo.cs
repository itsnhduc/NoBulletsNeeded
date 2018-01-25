using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowCryo : HeroAbility
{

    public float duration;
    public int healthEffect;
    public float effectInterval;

    private GameObject _target;

    public void Activate(GameObject target, GameObject initiator)
    {
        _target = target;
        SetHero(initiator);
        StartCoroutine("Freeze");
    }

    IEnumerator Freeze()
    {
        _target.GetComponent<Hero>().SetControllable(false);
        StartCoroutine("DealFreezingEffect");
        yield return new WaitForSeconds(duration);
        StopCoroutine("DealFreezingEffect");
        _target.GetComponent<Hero>().SetControllable(true);
        Destroy(gameObject);
    }

    IEnumerator DealFreezingEffect()
    {
        while (true)
        {
            int healthChange = _target.name == parentHero.name ? healthEffect : -healthEffect;
            _target.GetComponent<Mortality>().AlterHealth(healthChange, parentHero);
            yield return new WaitForSeconds(effectInterval);
        }
    }
}
