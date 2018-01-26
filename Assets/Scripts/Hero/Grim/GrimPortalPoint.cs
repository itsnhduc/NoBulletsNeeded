using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimPortalPoint : HeroAbility {
	
	public GameObject grab;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Ground")
		{
			GameObject grabObj = Instantiate(grab, transform.position, new Quaternion());
			grabObj.name = "GrimGrab";
			grabObj.GetComponent<HeroAbility>().parentHero = parentHero;
			Destroy(gameObject);
		}
	}

}
