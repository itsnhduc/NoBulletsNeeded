using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFrostBlast : HeroAbility
{
    public GameObject cryo;
    private List<string> _affected = new List<string> { "Hero" };

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_affected.Contains(other.tag) && other.name != parentHero.name)
        {
            GiveCryo(other.gameObject);
        }
    }

    public void GiveCryo(GameObject receiver)
    {
		if (receiver.GetComponentInChildren<SnowCryo>() == null)
		{
			GameObject cryoObj = Instantiate(cryo, receiver.transform.position, new Quaternion());
			cryoObj.name = "SnowCryo";
			cryoObj.transform.parent = receiver.transform;
			GameObject initiator = parentHero ? parentHero : receiver;
			cryoObj.GetComponent<SnowCryo>().Activate(receiver, initiator);
			if (parentHero) Destroy(gameObject);
		}
    }
}
